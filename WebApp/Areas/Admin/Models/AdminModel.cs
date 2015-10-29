using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using DataLayer;
using WebApp.Models;

namespace WebApp.Areas.Admin.Models
{
    public class AdminModel : MasterModel
    {
        public int UserId { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "Name Required")]
        public string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailId { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public AdminModel()
        {
            Title = "Admin: User";
            ActiveModel = "User";
        }
    }

    public class AdminModelMapper
    {
        public static AdminModel Map(User user)
        {
            var model = new AdminModel();
            if (user != null)
            {
                model.UserId = user.UserId;
                model.UserName = user.UserName;
                model.EmailId = user.EmailId;
                model.Password = user.Password;
            }
            return model;
        }

        public static User Map(AdminModel model)
        {
            var user = new User();
            {
                user.UserId = model.UserId;
                user.UserName = model.UserName;
                user.EmailId = model.EmailId;
                user.Password = model.Password;
            }
            return user;
        }
    }
}