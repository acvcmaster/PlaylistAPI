using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;
using PlaylistAPI.Services;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : BaseController<User, UserBusiness>
    {
        private readonly ICryptographyService cryptographyService;

        public UserController(PlaylistContext context, ICryptographyService cryptographyService) : base(context)
        {
            this.cryptographyService = cryptographyService;
        }

        [HttpPost]
        public override IActionResult Insert(User model)
        {
            model.Password = cryptographyService.GetSHA256(model.Password);
            return base.Insert(model);
        }
    }
}