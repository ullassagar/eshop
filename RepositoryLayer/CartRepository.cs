using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer;
using MongoRepository;

namespace RepositoryLayer
{
    public class CartRepository
    {
        public static Cart GetCart(int memberId)
        {
            var repository = new MongoRepository<Cart>();
            var cart = repository.FirstOrDefault(c => c.MemberId == memberId);
            return cart ?? new Cart { MemberId = memberId };
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
