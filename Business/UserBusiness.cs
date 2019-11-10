using System.Linq;
using PlaylistAPI.Models;

namespace PlaylistAPI.Business
{
    public class UserBusiness : BaseBusiness<User>
    {
        public UserBusiness() : base(null)
        {
        }
    }
}