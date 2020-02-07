using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
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

        public IEnumerable<int> GetSongIds(int id, HttpRequest request)
        {
            try
            {
                PlaylistRuleBusiness playlistRuleBusiness = GetAuxiliraryBusiness<PlaylistRuleBusiness, PlaylistRule>();
                var songBusiness = GetAuxiliraryBusiness<SongBusiness, Song>();
                var songSet = Context.ArquireDbSet<Song>();
                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();
                var hardCodedEntrySet = Context.ArquireDbSet<HardCodedEntry>();

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

                    return ids;
                }
                else
                    return (from e in hardCodedEntrySet where e.PlaylistId == id select e.SongId).Distinct().ToList();
            }
            catch { return null; }
        }

        public IEnumerable<CompleteSong> GetSongs(int id, HttpRequest request)
        {
            try
            {
                PlaylistRuleBusiness playlistRuleBusiness = GetAuxiliraryBusiness<PlaylistRuleBusiness, PlaylistRule>();
                var songBusiness = GetAuxiliraryBusiness<SongBusiness, Song>();
                var songSet = Context.ArquireDbSet<Song>();
                var songPropertySet = Context.ArquireDbSet<SongProperty>();
                var propertySet = Context.ArquireDbSet<Property>();
                var hardCodedEntrySet = Context.ArquireDbSet<HardCodedEntry>();

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


                List<CompleteSong> completeSongs = null;
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
                        completeSongs = songBusiness.GetSongsFromIds(ids, request);
                }
                else
                {
                    var ids = (from e in hardCodedEntrySet where e.PlaylistId == id select e.SongId).Distinct().ToList();
                    completeSongs = songBusiness.GetSongsFromIds(ids, request);
                }
                return completeSongs;
            }
            catch { return null; }
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

        public AmplitudeJSPlaylist GetAmplitudeJSPlaylist(int id, HttpRequest request)
        {
            try
            {
                var songBusiness = GetAuxiliraryBusiness<SongBusiness, Song>();
                var userBusiness = GetAuxiliraryBusiness<UserBusiness, User>();

                var playlist = base.Get(id);
                var ids = GetSongIds(id, request);
                var songs = songBusiness.GetAmplitudeJSSongsFromIds(ids, request);
                var author = userBusiness.Get(playlist.OwnerID);
                return new AmplitudeJSPlaylist() { Author = author.Name, Title = playlist.Name, Songs = songs };
            }
            catch { return null; }
        }

        #region Helpers
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
        #endregion
    }
}