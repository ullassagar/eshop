using DataLayer;
using MongoRepository;
using System.Linq;

namespace RepositoryLayer
{
    public class CartRepository : ICartRepository
    {
        public Cart GetCart(int memberId)
        {
            var repository = new MongoRepository<Cart>();
            Cart cart = repository.FirstOrDefault(c => c.MemberId == memberId);
            return cart ?? new Cart { MemberId = memberId };
        }

        public void SaveCart(Cart cart)
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