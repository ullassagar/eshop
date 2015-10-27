using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class MemberHandler
    {
        public static Member GetMember(int MemberId)
        {
            return MemberRepository.GetMember(MemberId);
        }

        public static Member GetMember(string emailAddress, string password)
        {
            return MemberRepository.GetMember(emailAddress, password);
        }

        public static ErrorCode AddMember(Member member)
        {
            if (string.IsNullOrEmpty(member.EmailAddress))
                return ErrorCode.ErrorWhileMemberRegistrationEmailEmpty;
            
            if (string.IsNullOrEmpty(member.Password))
                return ErrorCode.ErrorWhileMemberRegistrationPasswordEmpty;

            return MemberRepository.AddMember(member);
        }
    }
}
