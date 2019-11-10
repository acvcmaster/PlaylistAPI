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
    }
}