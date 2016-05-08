using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Model
{
    public class Receipt : Entity, IEquatable<Receipt>
    {
        private readonly IEnumerable<CartItem> items;
        private readonly IEnumerable<Discount> discounts;
        private readonly IEnumerable<Tax> taxes;

        public Receipt(Guid id, IEnumerable<CartItem> items, IEnumerable<Discount> discounts, IEnumerable<Tax> taxes)
            : base(id)
        {
            Guard.NotNull(items, "items");
            Guard.NotNull(discounts, "discounts");
            Guard.NotNull(taxes, "taxes");

            this.items = items;
            this.discounts = discounts;
            this.taxes = taxes;
        }

        public IEnumerable<CartItem> Items { get { return this.items; } }
        public IEnumerable<Tax> Taxes { get { return this.taxes; } }
        public IEnumerable<Discount> Discounts { get { return this.discounts; } }

        public decimal ItemsTotal
        {
            get
            {
                return this.Items
                           .Select(item => item.Total)
                           .Sum();
            }
        }

        public decimal DiscountTotal
        {
            get
            {
                return this.Discounts
                           .Select(discount => discount.Amount)
                           .Sum();
            }
        }

        public decimal TaxTotal
        {
            get
            {
                return this.Taxes
                           .Select(tax => tax.Amount)
                           .Sum();
            }
        }

        public decimal Total
        {
            get { return (this.ItemsTotal + this.TaxTotal) - this.DiscountTotal; }
        }

        public bool Equals(Receipt other)
        {
            if (other.IsNull())
            {
                return false;
            }
            return this.Id == other.Id;
        }

        public override string ToString()
        {
            return "Receipt [{0}]".FormatWith(this.Id);
        }
    }
}
