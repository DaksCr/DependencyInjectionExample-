using Sapiens.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens.Web.Model.ViewModels
{
    public class StoreViewModel
    {
        public StoreViewModel(Cart cart, IEnumerable<Product> products)
        {
            Guard.NotNull(cart, "cart");
            Guard.NotNull(products, "products");

            this.Cart = cart;
            this.Products = products;
        }

        public StoreViewModel(Cart cart, IEnumerable<Product> products, Receipt receipt)
            : this(cart, products)
        {
            Guard.NotNull(receipt, "receipt");
            this.Receipt = receipt;
        }

        public Cart Cart { get; private set; }
        public IEnumerable<Product> Products { get; private set; }
        public Receipt Receipt { get; private set; }
        public bool HasReceipt { get { return this.Receipt.IsNotNull(); } }
    }
}
