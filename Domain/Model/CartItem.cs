using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class CartItem : Entity, IEquatable<CartItem>
    {
        private readonly Product product;
        private int quantity;
 
        public CartItem(Guid id, Product product, int quantity)
            : base(id)
        {
            Guard.NotNull(product, "product");
            this.product = product;
            this.Quantity = quantity;
        }

        public int Quantity {
            get { return this.quantity; }
            set
            {
                Guard.NotNegativeOrZero(value, "quantity");
                this.quantity = value;
            }
        }
 
        public string Description { get { return this.product.Name; } }
        public decimal UnitPrice { get { return this.product.Price; } }
        public decimal Total { get { return this.UnitPrice * this.Quantity; } }

        public bool Equals(CartItem other)
        {
            if (other.IsNull())
            {
                return false;
            }
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return "{0} (${1}) [${2}]".FormatWith(this.Description, this.Quantity, this.UnitPrice);
        }
    }
}
