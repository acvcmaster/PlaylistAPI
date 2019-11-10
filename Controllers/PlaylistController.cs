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
    }
}