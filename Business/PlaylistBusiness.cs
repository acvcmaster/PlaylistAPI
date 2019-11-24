using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using PlaylistAPI.Models;
using PlaylistsNET.Content;
using PlaylistsNET.Models;

namespace PlaylistAPI.Business
{
    public class PlaylistBusiness : BaseBusiness<Playlist>
    {
        public PlaylistBusiness() : base(null)
        {

        }

        public IEnumerable<Playlist> GetAllFromUser(int Id)
        {
            var playlistSet = Context.ArquireDbSet<Playlist>();
            return playlistSet.Where(item => item.OwnerID == Id);
        }

        public IEnumerable<CompleteSong> GetSongs(int id, HttpRequest request)
        {
            try
            {
                PlaylistRuleBusiness playlistRuleBusiness = GetAuxiliraryBusiness<PlaylistRuleBusiness, PlaylistRule>();
                var songSet = Context.ArquireDbSet<Song>();
                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();

                var playlistRules = playlistRuleBusiness.GetPlaylistRules(id).ToList();
                var playlist = base.Get(id);

                Dictionary<int, IEnumerable<CompleteSong>> auxiliaryPlaylists = new Dictionary<int, IEnumerable<CompleteSong>>();

                foreach (var rule in playlistRules)
                    if (rule.Operator == "i" || rule.Operator == "!i")
                        auxiliaryPlaylists.Add(int.Parse(rule.Data), GetSongs(int.Parse(rule.Data), request));

                var songsAndRules = playlist.IsSmart ? (from rule in playlistRules
                                                        join sp in songPropertySet on rule.PropertyId equals sp.PropertyId
                                                        join p in propertySet on sp.PropertyId equals p.Id
                                                        where (p.Type == rule.PropertyType) && PropertyAccordingToRule(rule.PropertyType, rule.Operator,
                                                            sp.Value, rule.Data, sp.SongId, auxiliaryPlaylists)
                                                        select new { songId = sp.SongId, ruleId = rule.Id }).ToList() : null;


                List<CompleteSong> completeSongs = new List<CompleteSong>();
                if (songsAndRules != null)
                {
                    Dictionary<int, int> songsAndRulesDict = new Dictionary<int, int>();
                    foreach (var item in songsAndRules)
                    {
                        if (!songsAndRulesDict.ContainsKey(item.songId))
                            songsAndRulesDict.Add(item.songId, 0);

                        songsAndRulesDict[item.songId]++;
                    }

                    var ids = songsAndRules != null ?
                    (
                        from item in songsAndRulesDict
                        where (playlist.DisjunctiveRules && item.Value > 0) || (!playlist.DisjunctiveRules && item.Value == playlistRules.Count)
                        orderby item.Key
                        select item.Key
                    ).ToList() : null;

                    if (ids != null)
                    {
                        var songs = from song in songSet where ids.Contains(song.Id) select song;

                        var properties = (from property in songPropertySet
                                          join p in propertySet on property.PropertyId equals p.Id
                                          where ids.Contains(property.SongId)
                                          select new CompleteSongProperty
                                          {
                                              Id = property.Id,
                                              Creation = property.Creation,
                                              LastModification = property.LastModification,
                                              PropertyId = p.Id,
                                              Name = p.Name,
                                              Type = p.Type,
                                              Description = p.Description,
                                              SongId = property.SongId,
                                              Value = property.Value
                                          }).ToList();

                        foreach (var song in songs)
                        {
                            if (!File.Exists(song.Url))
                                continue;

                            var completeSong = new CompleteSong() { Song = song, Properties = properties.Where(item => item.SongId == song.Id).ToList() };
                            string contentType = new FileExtensionContentTypeProvider().TryGetContentType(song.Url, out contentType) ?
                                contentType : "application/octet-stream";

                            completeSong.Type = contentType;
                            if (request != null)
                                completeSong.RemoteUrl = $"{request.Scheme}://{request.Host}/Song/GetFile?id={song.Id}";
                            completeSongs.Add(completeSong);
                        }
                    }
                }
                else
                {
                    // TODO: Non-smart playlists
                }
                return completeSongs;
            }
            catch { return null; }
        }

        private bool PropertyAccordingToRule(string propertyType, string op, string value, string data, int songId, Dictionary<int, IEnumerable<CompleteSong>> auxiliaryPlaylists)
        {
            try
            {
                if (value != null || propertyType == "SONG")
                {
                    switch (propertyType)
                    {
                        case "STRING":
                            return CompareStrings(value, data, op);
                        case "DATETIME":
                            return CompareDatetimes(DateTime.Parse(value), DateTime.Parse(data), op);
                        case "INTEGER":
                            return CompareIntegers(int.Parse(value), int.Parse(data), op);
                        case "BOOLEAN":
                            return CompareBooleans(bool.Parse(value), bool.Parse(data), op);
                        case "SONG":
                            {
                                var contains = auxiliaryPlaylists[int.Parse(data)].Where(item => item.Song.Id == songId).FirstOrDefault() != null;
                                return op == "i" ? contains : !contains;
                            }
                    }
                }
                return false;
            }
            catch { return false; }
        }

        public PlaylistFile GetPlaylistFile(int id, HttpRequest request)
        {
            try
            {
                var basePlaylist = base.Get(id);
                if (basePlaylist == null)
                    return null;

                MemoryStream playlistStream = new MemoryStream();
                M3uPlaylist playlist = new M3uPlaylist() { IsExtended = true };
                var songs = GetSongs(id, request);

                foreach (var song in songs)
                {
                    playlist.PlaylistEntries.Add(new M3uPlaylistEntry()
                    {
                        Album = song.Properties.Where(item => item.Name == "ALBUM").FirstOrDefault()?.Value,
                        AlbumArtist = song.Properties.Where(item => item.Name == "ALBUM_ARTIST").FirstOrDefault()?.Value,
                        Path = song.RemoteUrl,
                        Title = $"{song.Properties.Where(item => item.Name == "NAME").FirstOrDefault()?.Value}"
                    });
                }
                var playlistText = PlaylistToTextHelper.ToText(playlist);
                var data = Encoding.Default.GetBytes(playlistText);
                return new PlaylistFile() { Data = data, Playlist = basePlaylist };
            }
            catch { return null; }
        }

        private bool CompareStrings(string a, string b, string op)
        {
            switch (op)
            {
                case "==":
                    return a.Equals(b);
                case "!=":
                    return !a.Equals(b);
                case "c":
                    return a.Contains(b);
                case "!c":
                    return !a.Contains(b);
            }
            return false;
        }

        private bool CompareDatetimes(DateTime a, DateTime b, string op)
        {
            switch (op)
            {
                case "==":
                    return a.Equals(b);
                case "!=":
                    return !a.Equals(b);
                case "b":
                    return (b - a).Ticks > 0;
                case "!b":
                    return (b - a).Ticks < 0;
            }
            return false;
        }

        private bool CompareIntegers(int a, int b, string op)
        {
            switch (op)
            {
                case ">":
                    return a > b;
                case ">=":
                    return a >= b;
                case "<":
                    return a < b;
                case "<=":
                    return a <= b;
                case "==":
                    return a.Equals(b);
                case "!=":
                    return !a.Equals(b);
            }
            return false;
        }

        private bool CompareBooleans(bool a, bool b, string op)
        {
            switch (op)
            {
                case "t":
                    return a;
                case "!t":
                    return !a;
            }
            return false;
        }
    }
}