using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
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
            SongBusiness songBusiness = GetAuxiliraryBusiness<SongBusiness, Song>();
            PlaylistRuleBusiness playlistRuleBusiness = GetAuxiliraryBusiness<PlaylistRuleBusiness, PlaylistRule>();

            var playlistRules = playlistRuleBusiness.GetPlaylistRules(id);
            return null;
        }
    }
}