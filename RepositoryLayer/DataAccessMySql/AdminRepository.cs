using DataLayer;
using MySql.Data.MySqlClient;
using System.Data;

namespace RepositoryLayer
{
    public class AdminRepository : IAdminRepository
    {
        public User GetUser(int userId)
        {
            User user = null;
            var sql = string.Format(@"SELECT UserId, UserName, Password, EmailId FROM users WHERE UserId={0}", userId);
            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    user = User.Load(reader);
                }
            }
            return user;
        }

        public User GetUser(string emailAddress, string password)
        {
            User user = null;
            var sql = string.Format(@"SELECT UserId, UserName, Password, EmailId FROM users WHERE EmailId='{0}' And Password='{1}'", emailAddress, password);
            using (var reader = MysqlRepository.ExecuteReader(MysqlRepository.ConnectionString_ReadOnly, CommandType.Text, sql))
            {
                if (reader.Read())
                {
                    user = User.Load(reader);
                }
            }
            return user;
        }

        public void Update(User user)
        {
            var sql = string.Format(@"UPDATE Users SET UserName='{0}', EmailId='{1}', Password='{2}' WHERE UserId={3}", user.UserName, user.EmailId, user.Password, user.UserId);
            MysqlRepository.ExecuteNonQueryAndCloseConnection(MysqlRepository.ConnectionString_Writable, sql, null);
        }
    }
}