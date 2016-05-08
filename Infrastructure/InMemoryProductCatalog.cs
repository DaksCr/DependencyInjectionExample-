using Sapiens.Domain.Adapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sapiens.Domain.Model;

namespace Sapiens.Infrastructure
{
    public class InMemoryProductCatalog : IProductCatalog
    {
        private readonly IEnumerable<Product> products;

        public InMemoryProductCatalog(IEnumerable<Product> products = null)
        {
            this.products = products ?? new List<Product>()
            {
                new Product(Guid.NewGuid(), "Producto 1", "Este es el producto 1", 1000),
                new Product(Guid.NewGuid(), "Producto 2", "Este es el producto 2", 2000),
                new Product(Guid.NewGuid(), "Producto 3", "Este es el producto 3", 3000),
                new Product(Guid.NewGuid(), "Producto 4", "Este es el producto 4", 4000),
                new Product(Guid.NewGuid(), "Producto 5", "Este es el producto 5", 5000),
                new Product(Guid.NewGuid(), "Producto 6", "Este es el producto 6", 6000),
            };
        }

        public IEnumerable<Product> Products()
        {
            return products;
        }
    }
}
