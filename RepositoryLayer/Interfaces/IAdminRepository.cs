using DataLayer;

namespace RepositoryLayer
{
    public interface IAdminRepository : Repositories.IRepository<User>
    {
        User GetUser(int userId);
        User GetUser(string emailAddress, string password);
    }
}