using System.ComponentModel.DataAnnotations;
using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class Member
    {
        [Key]
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public static Member Load(IDataReader reader)
        {
            return new Member
            {
                MemberId = DbHelper.ConvertToInt32(reader["MemberId"]),
                MemberName = DbHelper.ConvertToString(reader["MemberName"]),
                EmailAddress = DbHelper.ConvertToString(reader["EmailAddress"]),
                Password = DbHelper.ConvertToString(reader["Password"])
            };
        }
    }
}