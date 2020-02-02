using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class HardCodedEntryController : BaseController<HardCodedEntry, HardCodedEntryBusiness>
    {
        public HardCodedEntryController(PlaylistContext context) : base(context)
        {
        }
    }
}