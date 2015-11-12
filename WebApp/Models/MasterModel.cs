
namespace WebApp.Models
{
    public class MasterModel
    {
        public string Title { get; set; }
        public string ActiveModel { get; set; }
        public string LoggedInMemberName { get; set; }

        public MasterModel()
        {
            Title = "Home";
            ActiveModel = "Home";
            var publicUser = PublicUser.GetCurrentUser();
            if (publicUser != null)
            {
                LoggedInMemberName = publicUser.MemberName;
            }
        }
    }
}