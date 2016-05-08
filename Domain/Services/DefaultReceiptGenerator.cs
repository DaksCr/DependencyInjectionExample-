using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Domain.Services
{
    public class DefaultReceiptGenerator : IReceiptGenerator
    {
        private readonly ITaxCalculator taxCalculator;
        private readonly IDiscountCalculator discountCalculator;

        public DefaultReceiptGenerator(ITaxCalculator taxCalculator, IDiscountCalculator discountCalculator)
        {
            Guard.NotNull(taxCalculator, "taxCalculator");
            Guard.NotNull(discountCalculator, "discountCalculator");

            this.taxCalculator = taxCalculator;
            this.discountCalculator = discountCalculator;
        }

        public Receipt Generate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            IEnumerable<Tax> taxes = this.taxCalculator.Calculate(cart);
            IEnumerable<Discount> discounts = this.discountCalculator.Calculate(cart);

            return new Receipt(Guid.NewGuid(), cart.Items, discounts, taxes);
        }
    }
}
