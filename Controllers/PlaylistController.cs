using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;
using Newtonsoft.Json;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PlaylistController : BaseController<Playlist, PlaylistBusiness>
    {

        public PlaylistController(PlaylistContext context) : base(context)
        {
            Business.AddAuxiliraryBusiness<PlaylistRuleBusiness, PlaylistRule>();
            Business.AddAuxiliraryBusiness<SongBusiness, Song>();
            Business.AddAuxiliraryBusiness<UserBusiness, User>();
        }

        [HttpGet]
        public IActionResult GetAllFromUser([FromQuery]int id)
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
        public IActionResult GetSongs([FromQuery]int id)
        {
            try
            {
                var result = Business.GetSongs(id, this.HttpContext.Request);
                if (result != null)
                    return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
                return NoContent();
            }
            catch { return BadRequest($"Could not get songs by playlist id."); }
        }

        [HttpGet]
        public IActionResult GetPlaylistFile([FromQuery]int id)
        {
            try
            {
                var result = Business.GetPlaylistFile(id, this.HttpContext.Request);
                if (result != null)
                    return File(result.Data, "audio/x-mpegurl", result.Playlist.Name, true);
                return NoContent();
            }
            catch { return BadRequest($"Could not get playlist file by playlist id."); }
        }

        [HttpGet]
        public IActionResult GetAmplitudeJSPlaylist([FromQuery]int id)
        {
            try
            {
                var result = Business.GetAmplitudeJSPlaylist(id, this.HttpContext.Request);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get playlist by id."); }
        }

        [HttpGet]
        public IActionResult GetAllAmplitudeJSPlaylist()
        {
            try
            {
                var result = Business.GetAllAmplitudeJSPlaylist(this.HttpContext.Request);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get playlists."); }
        }
    }
}