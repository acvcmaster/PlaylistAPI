using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class PlaylistBusiness : BaseBusiness<Playlist>
    {
        public PlaylistBusiness() : base(null)
        {
        }

        public IQueryable<Playlist> GetAllFromUser(int Id)
        {
            var playlistSet = Context.ArquireDbSet<Playlist>();
            return playlistSet.Where(item => item.OwnerID == Id);
        }
    }
}