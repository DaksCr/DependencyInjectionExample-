using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Domain.Services
{
    public class CompositeDiscountCalculator : IDiscountCalculator
    {
        private readonly IEnumerable<IDiscountCalculator> calculators;

        public CompositeDiscountCalculator(params IDiscountCalculator[] calculators)
            : this(calculators.AsEnumerable())
        { }

        public CompositeDiscountCalculator(IEnumerable<IDiscountCalculator> calculators)
        {
            Guard.NotNull(calculators, "calculators");
            this.calculators = calculators;
        }

        public IEnumerable<Discount> Calculate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            return this.calculators.SelectMany(calculator => calculator.Calculate(cart));
        }
    }
}
