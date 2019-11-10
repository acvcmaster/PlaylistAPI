using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PlaylistController : BaseController<Playlist>
    {
        public PlaylistController(PlaylistContext context) : base(context)
        {
            this.AcquireBusiness<PlaylistBusiness>();
        }
    }
}