using Microsoft.AspNetCore.Mvc;

namespace PlaylistAPI.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RedirectController : ControllerBase
    {
        [Route("/")]
        [Route("/docs")]
        [Route("/swagger")]
        public IActionResult Index()
        {
            return new RedirectResult("/swagger");
        }
    }
}