using System.Data;
using UtilityLayer;

namespace DataLayer
{
    public class Member
    {
        public int MemberId { get; set; }
        public string MemberName { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }

        public static Member Load(IDataReader reader)
        {
            var member = new Member();
            member.MemberId = DbHelper.ConvertToInt32(reader["MemberId"]);
            member.MemberName = DbHelper.ConvertToString(reader["MemberName"]);
            member.EmailAddress = DbHelper.ConvertToString(reader["EmailAddress"]);
            member.Password = DbHelper.ConvertToString(reader["Password"]);
            return member;
        }
    }
}
