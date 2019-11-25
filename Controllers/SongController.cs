using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        [HttpGet]
        [Produces("application/octet-stream", "application/json")]
        public IActionResult GetFile([FromQuery]int id)
        {
            var file = Business.GetFile(id);
            if (file != null)
            {
                return File(file.File, file.Type, file.Properties.FirstOrDefault().Value, true);
            }
            return NotFound("Could not get song file by id.");
        }

        [HttpPost]
        public IActionResult MassInsert([FromQuery]string directoryUrl)
        {
            var result = Business.MassInsert(directoryUrl);
            if (result != null)
                return Ok(result);
            return BadRequest("Failed to mass insert songs (directory exists?).");
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery]int page, [FromQuery]int entriesPerPage)
        {
            var result = Business.GetAll(page, entriesPerPage, this.HttpContext.Request);
            if (result != null)
                return Ok(JsonConvert.SerializeObject(result, Formatting.Indented));
            return BadRequest("Could not get songs.");
        }
    }
}