using System.Linq;
using DataLayer;
using MongoRepository;

namespace RepositoryLayer
{
    public class CartRepository
    {
        public static Cart GetCart(int memberId)
        {
            var repository = new MongoRepository<Cart>();
            Cart cart = repository.FirstOrDefault(c => c.MemberId == memberId);
            return cart ?? new Cart {MemberId = memberId};
        }

        public static void SaveCart(Cart cart)
        {
            var repository = new MongoRepository<Cart>();
            if (string.IsNullOrEmpty(cart.Id))
            {
                repository.Add(cart);
            }
            else
            {
                repository.Update(cart);
            }
        }
    }
}