using DataLayer;
using System.Web;

namespace WebApp
{
    public class AdminUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string EmailId { get; set; }

        public static AdminUser GetCurrentUser()
        {

            return (AdminUser)HttpContext.Current.Session[Constants.AdminUserKeyName];

            //return new AdminUser { UserId = 1, UserName = "Admin" };
            //return (AdminUser)HttpContext.Current.Session[Constants.AdminUserKeyName];
        }

        public static AdminUser GetCurrentUser(User user)
        {
            return new AdminUser { UserId = user.UserId, UserName = user.UserName, EmailId = user.EmailId };
        }
    }
}