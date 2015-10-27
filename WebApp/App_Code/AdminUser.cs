namespace WebApp
{
    public class AdminUser
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public static AdminUser GetCurrentUser()
        {
            return new AdminUser { UserId = 1, UserName = "Admin" };
            //return (AdminUser)HttpContext.Current.Session[Constants.AdminUserKeyName];
        }
    }
}