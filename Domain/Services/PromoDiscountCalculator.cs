using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Domain.Services
{
    public class PromoDiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<string> codes;
        private readonly int percentage;

        public PromoDiscountCalculator(int promoPercentage, params string[] promoCodes)
            : this(promoPercentage, promoCodes.AsEnumerable())
        { }

        public PromoDiscountCalculator(int promoPercentage, IEnumerable<string> promoCodes)
        {
            Guard.NotNegativeOrZero(promoPercentage, "promoPercentage");
            Guard.NotNull(promoCodes, "promoCodes");

            this.percentage = promoPercentage;
            this.codes = promoCodes;
        }

        public IEnumerable<string> Codes { get { return this.codes; } }
        public int Percentage { get { return this.percentage; } }

        public IEnumerable<Discount> Calculate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            if (cart.Promo.IsNullOrEmpty() || !this.Codes.Contains(cart.Promo))
            {
                return Enumerable.Empty<Discount>();
            }

            decimal total = cart.Items.Select(item => item.Total).Sum();
            string description = "Descuento por promo: {0}".FormatWith(cart.Promo);
            return new[]
            {
                new Discount(cart.Promo, description, this.Calculate(total, this.percentage)),
            };
        }

        private decimal Calculate(decimal total, int percentage)
        {
            return total * (percentage / (decimal)100.0);
        }
    }
}
