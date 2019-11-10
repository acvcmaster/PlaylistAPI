using Microsoft.AspNetCore.Mvc;
using PlaylistAPI.Business;
using PlaylistAPI.Models;

namespace PlaylistAPI.Controllers
{
    [Route("[controller]/[action]")]
    public class UserController : BaseController<User>
    {
        public UserController(PlaylistContext context) : base(context)
        {
            this.AcquireBusiness<UserBusiness>();
        }
    }
}