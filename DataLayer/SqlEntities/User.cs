using System.ComponentModel.DataAnnotations;
using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string EmailId { get; set; }

        public static User Load(IDataReader reader)
        {
            return new User
            {
                UserId = DbHelper.ConvertToInt32(reader["UserId"]),
                UserName = DbHelper.ConvertToString(reader["UserName"]),
                Password = DbHelper.ConvertToString(reader["Password"]),
                EmailId = DbHelper.ConvertToString(reader["EmailId"])
            };
        }
    }
}