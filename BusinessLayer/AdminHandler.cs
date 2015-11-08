using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
    public class AdminHandler
    {
        private readonly IAdminRepository _adminRepository;

        public AdminHandler()
        {
            _adminRepository = new AdminRepository();
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
