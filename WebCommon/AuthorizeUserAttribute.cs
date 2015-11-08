using System;
using System.Web;
using System.Web.Mvc;

namespace WebApp
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeUserAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            httpContext.Session[Constants.AppUserKeyName] = PublicUser.GetCurrentUser();
            if (httpContext.Session[Constants.AppUserKeyName] == null)
            {
                return false;
            }
            else
            {
                var user = (PublicUser)httpContext.Session[Constants.AppUserKeyName];
                return user != null;
            }
        }
    }
}