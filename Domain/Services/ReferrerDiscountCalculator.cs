using Sapiens.Domain.Adapters;
using Sapiens.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Domain.Services
{
    public class ReferrerDiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<string> referrers;
        private readonly decimal amount;

        public ReferrerDiscountCalculator(decimal referrerAmount, params string[] referrers)
            : this(referrerAmount, referrers.AsEnumerable())
        { }

        public ReferrerDiscountCalculator(decimal referrerAmount, IEnumerable<string> referrers)
        {
            Guard.NotNegativeOrZero(referrerAmount, "referrerAmount");
            Guard.NotNull(referrers, "referrers");

            this.amount = referrerAmount;
            this.referrers = referrers;
        }

        public IEnumerable<string> Referrers { get { return this.referrers; } }
        public decimal Amount { get { return this.amount; } }

        public IEnumerable<Discount> Calculate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            if (cart.Referrer.IsNullOrEmpty() || !this.Referrers.Contains(cart.Referrer))
            {
                return Enumerable.Empty<Discount>();
            }
 
            string description = "Descuento por referencia: {0}".FormatWith(cart.Referrer);
            return new[] { new Discount(cart.Referrer, description, this.amount) };
        }
    }
}
