using System;
using System.Web;
using System.Web.Mvc;

namespace WebApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAdminAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            httpContext.Session[Constants.AdminUserKeyName] = AdminUser.GetCurrentUser();
            if (httpContext.Session[Constants.AdminUserKeyName] == null)
            {
                return false;
            }
            else
            {
                var user = (AdminUser)httpContext.Session[Constants.AdminUserKeyName];
                return user != null;
            }
        }
    }
}