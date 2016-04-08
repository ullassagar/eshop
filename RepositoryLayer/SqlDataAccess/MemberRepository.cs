using DataLayer;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class MemberRepository : GenericSqlRepository<Member>, IMemberRepository
    {
        public MemberRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public Member GetMember(int memberId)
        {
            return GetById(memberId);
        }

        public Member GetMember(string emailAddress, string password)
        {
            return Get(m => m.EmailAddress == emailAddress && m.Password == password);
        }

        public ErrorCode AddMember(Member member)
        {
            if (Get(m => m.EmailAddress == member.EmailAddress) != null)
            {
                return ErrorCode.ErrorWhileMemberRegistrationEmailAlreadyExist;
            }

            Add(member);

            return ErrorCode.None;
        }
    }
}