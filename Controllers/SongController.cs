using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PlaylistAPI.Business;
using PlaylistAPI.Models;
using PlaylistAPI.Services;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class SongController : BaseController<Song, SongBusiness>
    {
        public readonly IThumbnailingService thumbnailingService;

        public SongController(PlaylistContext context, IThumbnailingService thumbnailingService) : base(context)
        {
            this.thumbnailingService = thumbnailingService;
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

        [HttpGet]
        [Produces("image/jpeg", "application/json")]
        public IActionResult GetCoverArt([FromQuery]int id)
        {
            var song = Business.Get(id);
            if (song != null) {
                var result = thumbnailingService.GetThumbnail(song);
                if (result != null) {
                    return File(result, "image/jpeg", true);
                }
                return NotFound("Could not get cover art by song id.");
            }
            return NotFound("Could not get cover art by song id.");
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

        [HttpGet]
        public IActionResult GetAmplitudeJSSong([FromQuery]int id)
        {
            var result = Business.GetAmplitudeJSSong(id, this.HttpContext.Request);
            if (result != null)
                return Ok(result);
            return BadRequest("Could not get song by id.");
        }
    }
}