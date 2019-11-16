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
                var models = (from song in songSet select song).ToList();
                var playlist = base.Get(id);

                var properties = (from property in songPropertySet
                                  join p in propertySet on property.PropertyId equals p.Id
                                  orderby property.Id
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

                List<CompleteSong> completeSongs = new List<CompleteSong>();
                foreach (var model in models)
                {
                    if (!File.Exists(model.Url))
                        continue;

                    var completeSong = new CompleteSong() { Song = model, Properties = properties.Where(item => item.SongId == model.Id) };
                    if (IsInPlaylist(playlist, completeSong, playlistRules))
                    {
                        string contentType = new FileExtensionContentTypeProvider().TryGetContentType(model.Url, out contentType) ?
                            contentType : "application/octet-stream";

                        completeSong.Type = contentType;
                        completeSong.RemoteUrl = $"{request.Scheme}://{request.Host}/Song/GetFile?id={model.Id}";
                        completeSongs.Add(completeSong);
                    }
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
                        Album = song.Properties.Where(item => item.Name == "ALBUM").FirstOrDefault().Value,
                        AlbumArtist = song.Properties.Where(item => item.Name == "ALBUM_ARTIST").FirstOrDefault().Value,
                        Path = song.RemoteUrl,
                        Title = song.Song.Id.ToString()
                    });
                }
                var playlistText = PlaylistToTextHelper.ToText(playlist);
                var data = Encoding.Default.GetBytes(playlistText);
                playlistStream.Write(data, 0, data.Length);
                playlistStream.Seek(0, SeekOrigin.Begin);
                return new PlaylistFile() { Data = data, Playlist = basePlaylist };
            }
            catch { return null; }
        }

        private bool IsInPlaylist(Playlist playlist, CompleteSong song, IEnumerable<PlaylistRuleCompleteModel> playlistRules)
        {
            if (!playlist.IsSmart)
                return false;
            
            if (playlist.DisjunctiveRules)
            {
                foreach (var rule in playlistRules)
                    if (CheckRule(song, rule))
                        return true; // returns true if ANY rules are satisfied
                return false;
            }
            else
            {
                foreach (var rule in playlistRules)
                    if (!CheckRule(song, rule))
                        return false;
                return true; // returns true if ALL rules are satisfied
            }
        }

        private bool CheckRule(CompleteSong song, PlaylistRuleCompleteModel rule)
        {
            try
            {
                var property = song.Properties.Where(item => item.PropertyId == rule.PropertyId).FirstOrDefault();
                if (property != null && property.Type == rule.PropertyType)
                {
                    switch (rule.PropertyType)
                    {
                        case "STRING":
                            return CompareStrings(property.Value, rule.Data, rule.Operator);
                        case "DATETIME":
                            return CompareDatetimes(DateTime.Parse(property.Value), DateTime.Parse(rule.Data), rule.Operator);
                        case "INTEGER":
                            return CompareIntegers(int.Parse(property.Value), int.Parse(rule.Data), rule.Operator);
                        case "BOOLEAN":
                            return CompareBooleans(bool.Parse(property.Value), bool.Parse(rule.Data), rule.Operator);
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
    }
}