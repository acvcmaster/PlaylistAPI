using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class SongController : BaseController<Song, SongBusiness>
    {
        public SongController(PlaylistContext context) : base(context)
        {
        }
    }
}