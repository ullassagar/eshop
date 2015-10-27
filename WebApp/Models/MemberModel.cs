using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class MemberModel :MasterModel
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
            var member = new Member();
            {
                member.MemberId = model.MemberId;
                member.MemberName = model.MemberName;
                member.EmailAddress = model.EmailAddress;
                member.Password = model.Password;
            }
            return member;
        }
    }


}