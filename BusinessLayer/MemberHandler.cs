using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class MemberHandler
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MemberHandler(IMemberRepository memberRepository, IUnitOfWork unitOfWork)
        {
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
        }

        public Member GetMember(int memberId)
        {
            return _memberRepository.GetMember(memberId);
        }

        public Member GetMember(string emailAddress, string password)
        {
            return _memberRepository.GetMember(emailAddress, password);
        }

        public ErrorCode AddMember(Member member)
        {
            if (string.IsNullOrEmpty(member.EmailAddress))
                return ErrorCode.ErrorWhileMemberRegistrationEmailEmpty;

            if (string.IsNullOrEmpty(member.Password))
                return ErrorCode.ErrorWhileMemberRegistrationPasswordEmpty;

            return _memberRepository.AddMember(member);
        }

        public void Update(Member member)
        {
            _memberRepository.Update(member);
        }

    }
}
