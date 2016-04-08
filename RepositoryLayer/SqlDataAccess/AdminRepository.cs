using DataLayer;
using MySql.Data.MySqlClient;
using System.Data;
using RepositoryLayer.Infrastructure;

namespace RepositoryLayer
{
    public class AdminRepository : GenericSqlRepository<User>, IAdminRepository
    {
        public AdminRepository(IDatabaseFactory databaseFactory)
            : base(databaseFactory)
        {
        }

        public User GetUser(int userId)
        {
            return GetById(userId);
        }

        public User GetUser(string emailAddress, string password)
        {
            return Get(user => user.EmailId == emailAddress && user.Password == password);
        }

        public void Update(User user)
        {
            Update(user);
        }
    }
}