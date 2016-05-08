using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Infrastructure
{
    public class InMemoryCartStorage : ICartStorage
    {
        private readonly IDictionary<Guid, Cart> carts;

        public InMemoryCartStorage(IDictionary<Guid, Cart> carts = null)
        {
            this.carts = carts ?? new Dictionary<Guid, Cart>();
        }

        public Cart Restore(Guid id)
        {
            Cart cart;
            if (this.carts.TryGetValue(id, out cart))
            {
                return cart;
            }

            cart = new Cart(id);
            this.carts.Add(id, cart);
            return cart;
        }

        public void Save(Cart cart)
        {
            this.carts[cart.Id] = cart;
        }
    }
}
