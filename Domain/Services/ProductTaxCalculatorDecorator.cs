using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Domain.Services
{
    public class ProductTaxCalculatorDecorator : ITaxCalculator
    {
        private readonly ITaxCalculator calculator;
        private readonly IEnumerable<string> products;
        private readonly int percentage;

        public ProductTaxCalculatorDecorator(ITaxCalculator calculator, int productTaxPercentage, params string[] taxProducts)
            : this(calculator, productTaxPercentage, taxProducts.AsEnumerable())
        { }

        public ProductTaxCalculatorDecorator(ITaxCalculator calculator, int productTaxPercentage, IEnumerable<string> taxProducts)
        {
            Guard.NotNegativeOrZero(productTaxPercentage, "productTaxPercentage");
            Guard.NotNull(taxProducts, "taxProducts");
            Guard.NotNull(calculator, "calculator");

            this.calculator = calculator;
            this.percentage = productTaxPercentage;
            this.products = taxProducts;
        }

        public IEnumerable<string> Products { get { return this.products; } }
        public int Percentage { get { return this.percentage; } }

        public IEnumerable<Tax> Calculate(Cart cart)
        {
            Guard.NotNull(cart, "cart");
            CartItem item = cart.Items.FirstOrDefault(i => this.Products.Contains(i.Description));
            if (item.IsNull())
            {
                return Enumerable.Empty<Tax>();
            }

            string description = "Impuesto a Producto: {0}".FormatWith(item.Description);
            Tax tax = new Tax(description, this.Calculate(item.Total, this.percentage));
            return this.calculator.Calculate(cart).Concat(new[] { tax });
        }

        private decimal Calculate(decimal total, int percentage)
        {
            return total * (percentage / (decimal)100.0);
        }
    }
}
