using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class PlaylistRuleController : BaseController<PlaylistRule, PlaylistRuleBusiness>
    {
        public PlaylistRuleController(PlaylistContext context) : base(context)
        {
        }

        [HttpGet]
        public virtual IActionResult GetPlaylistRules([FromQuery]int id)
        {
            try
            {
                var result = Business.GetPlaylistRules(id);
                if (result != null)
                    return Ok(result);
                return NoContent();
            }
            catch { return BadRequest($"Could not get playlist rules by id."); }
        }
    }
}