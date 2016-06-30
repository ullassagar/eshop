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
    }
}