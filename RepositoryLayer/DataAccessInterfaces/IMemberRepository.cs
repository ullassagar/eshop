using DataLayer;

namespace RepositoryLayer
{
    public interface IMemberRepository
    {
        Member GetMember(int memberId);
        Member GetMember(string emailAddress, string password);
        ErrorCode AddMember(Member member);
        void Update(Member member);
    }
}