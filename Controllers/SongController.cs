using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;
using Newtonsoft.Json;
using System.Collections.Generic;

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
                return File(file.File, file.Type, true);
            }
            return NotFound("Could not get song file by id.");
        }
    }
}