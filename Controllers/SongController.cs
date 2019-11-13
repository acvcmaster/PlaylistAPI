using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;
using Newtonsoft.Json;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class SongController : BaseController<Song, SongBusiness>
    {
        public SongController(PlaylistContext context) : base(context)
        {
        }

        [HttpGet]
        [Produces("application/octet-stream", "application/json")]
        public IActionResult GetFile(int id)
        {
            var file = Business.GetFile(id);
            if (file != null)
            {
                return File(file.File, file.Type, true);
            }
            return NotFound("Could not get song file by id.");
        }

        [HttpGet]
        public IActionResult GetComplete(int id)
        {
            var result = Business.GetComplete(id, this.HttpContext.Request);
            if (result != null)
            {
                return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            }
            return BadRequest("Could not get song file by id.");
        }
    }
}