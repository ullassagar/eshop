using DataLayer;
using RepositoryLayer;
using RepositoryLayer.Infrastructure;

namespace BusinessLayer
{
    public class AdminHandler
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AdminHandler(IAdminRepository adminRepository, IUnitOfWork unitOfWork)
        {
            _adminRepository = adminRepository;
            _unitOfWork = unitOfWork;
        }

        public User GetUser(int userId)
        {
            return _adminRepository.GetUser(userId);
        }

        public User GetUser(string emailAddress, string password)
        {
            return _adminRepository.GetUser(emailAddress, password);
        }

        public void Update(User user)
        {
            _adminRepository.Update(user);
        }
    }
}
