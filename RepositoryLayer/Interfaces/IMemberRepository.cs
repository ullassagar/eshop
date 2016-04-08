using DataLayer;
using MongoRepository;

namespace RepositoryLayer
{
    public interface IMemberRepository :  Repositories.IRepository<Member>
    {
        Member GetMember(int memberId);
        Member GetMember(string emailAddress, string password);
        ErrorCode AddMember(Member member);
    }
}