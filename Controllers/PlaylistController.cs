using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PlaylistController : BaseController<Playlist, PlaylistBusiness>
    {

        public PlaylistController(PlaylistContext context) : base(context)
        {
            Business.AddAuxiliraryBusiness<SongBusiness, Song>();
        }

        [HttpGet]
        public virtual IActionResult GetAllFromUser([FromQuery]int id)
        {
            try
            {
                var result = Business.GetAllFromUser(id);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get {typeof(Playlist).Name} by user id."); }
        }

        [HttpGet]
        public virtual IActionResult GetSongs([FromQuery]int id)
        {
            try
            {
                var result = Business.GetSongs(id, this.HttpContext.Request);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get songs by playlist id."); }
        }
    }
}