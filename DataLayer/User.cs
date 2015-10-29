using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }

        public static User Load(IDataReader reader)
        {
            var user = new User();
            user.UserId = DbHelper.ConvertToInt32(reader["UserId"]);
            user.UserName = DbHelper.ConvertToString(reader["UserName"]);
            user.Password = DbHelper.ConvertToString(reader["Password"]);
            user.EmailId = DbHelper.ConvertToString(reader["EmailId"]);
            return user;
        }
    }
}
