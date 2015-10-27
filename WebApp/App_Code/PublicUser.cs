using System.Web;
using DataLayer;

namespace WebApp
{
    public class PublicUser
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string EmailAddress { get; set; }

        public static PublicUser GetCurrentUser()
        {
            return (PublicUser)HttpContext.Current.Session[Constants.AppUserKeyName];
        }

        public static PublicUser GetCurrentUser(Member member)
        {
            return new PublicUser { MemberId = member.MemberId, MemberName = member.MemberName, EmailAddress = member.EmailAddress };
        }
    }
}