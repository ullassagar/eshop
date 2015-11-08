using DataLayer;

namespace RepositoryLayer
{
    public interface ICartRepository
    {
        Cart GetCart(int memberId);
        void SaveCart(Cart cart);
    }
}