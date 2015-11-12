using System.ComponentModel.DataAnnotations;
using DataLayer;

namespace WebApp.Models.Customers
{
    public class MemberModel : MasterModel
    {
        public int MemberId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Name Required")]
        public string MemberName { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public MemberModel()
        {
            Title = "Admin: User";
            ActiveModel = "User";
        }
    }

    public class MemberModeMapper
    {
        public static MemberModel Map(Member member)
        {
            var model = new MemberModel();
            if (member != null)
            {
                model.MemberId = member.MemberId;
                model.MemberName = member.MemberName;
                model.EmailAddress = member.EmailAddress;
                model.Password = member.Password;
            }
            return model;
        }

        public static Member Map(MemberModel model)
        {
            return new Member
            {
                MemberId = model.MemberId, 
                MemberName = model.MemberName, 
                EmailAddress = model.EmailAddress,
                Password = model.Password
            };
        }
    }
}