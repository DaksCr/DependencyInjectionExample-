using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class Cart : Entity, IEquatable<Cart>
    {
        private readonly IList<CartItem> items;
        private string referrer = String.Empty;
        private string promo = String.Empty;
 
        public Cart(Guid id, IEnumerable<CartItem> items = null)
            : base(id)
        {
            this.items = items.IsNotNull()
                       ? items.ToList()
                       : new List<CartItem>();
        }

        public IEnumerable<CartItem> Items { get { return this.items.AsEnumerable(); } }

        public bool IsEmpty { get { return !this.Items.Any(); } }

        public string Referrer
        {
            get { return this.referrer; }
            set { this.referrer = value ?? String.Empty; }
        }

        public string Promo
        {
            get { return this.promo; }
            set { this.promo = value ?? String.Empty; }
        }

        public void Set(Product product, int quantity)
        {
            if (!this.items.Any(item => item.Id == product.Id))
            {
                this.items.Add(new CartItem(product.Id, product, quantity));
                return;
            }
            CartItem current = this.items.Single(item => item.Id == product.Id);
            current.Quantity = quantity;
        }

        public void Remove(Guid id)
        {
            CartItem current = this.items.FirstOrDefault(item => item.Id == id);
            if (current.IsNull())
            {
                return;
            }
            this.items.Remove(current);
        }

        public void Clear()
        {
            this.items.Clear();
        }

        public bool Equals(Cart other)
        {
            if (other.IsNull())
            {
                return false;
            }
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return "Cart [{0}]".FormatWith(this.Id);
        }
    }
}
