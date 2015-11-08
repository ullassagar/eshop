using DataLayer;

namespace RepositoryLayer
{
    public interface IAdminRepository
    {
        User GetUser(int userId);
        User GetUser(string emailAddress, string password);
        void Update(User user);
    }
}