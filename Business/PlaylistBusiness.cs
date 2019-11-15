using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using PlaylistAPI.Models;

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
            var property = song.Properties.Where(item => item.Id == rule.PropertyId).FirstOrDefault();
            if (property != null && property.Type == rule.PropertyType)
            {
                // var propertyValue = 
            }
            return false;
        }
    }
}