using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Domain.Services
{
    public class SalesTaxCalculator : ITaxCalculator
    {
        private readonly int percentage;

        public SalesTaxCalculator(int salesTaxPercentage)
        {
            Guard.NotNegativeOrZero(salesTaxPercentage, "salesTaxPercentage");
            this.percentage = salesTaxPercentage;
        }

        public int Percentage { get { return this.percentage; } }

        public IEnumerable<Tax> Calculate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            decimal total = cart.Items.Select(item => item.Total).Sum();
            return new[] 
            {
                new Tax("Impuesto de Venta", this.Calculate(total, percentage)),
            };
        }

        private decimal Calculate(decimal total, int percentage)
        {
            return total * (percentage / (decimal)100.0);
        }
    }
}
