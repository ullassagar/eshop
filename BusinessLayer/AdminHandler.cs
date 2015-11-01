using DataLayer;
using RepositoryLayer;

namespace BusinessLayer
{
   public class AdminHandler
    {
       public static User GetUser(int UserId)
       {
           return AdminRepository.GetUser(UserId);
       }

       public static User GetUser(string emailAddress, string password)
       {
           return AdminRepository.GetUser(emailAddress, password);
       }

       public static void Update(User user)
       {
           AdminRepository.Update(user);

       }
    }
}
