using Sapiens.Domain.Adapters;
using Sapiens.Domain.Model;
using Sapiens.Web.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sapiens.Web.Model.Controllers
{
    public class StoreController : Controller
    {
        private readonly ICartStorage storage;
        private readonly IProductCatalog catalog;
        private readonly IReceiptGenerator receiptGenerator;

        public StoreController(ICartStorage storage,
                               IProductCatalog catalog,
                               IReceiptGenerator receiptGenerator)
        {
            Guard.NotNull(storage, "storage");
            Guard.NotNull(catalog, "catalog");
            Guard.NotNull(receiptGenerator, "receiptGenerator");

            this.storage = storage;
            this.catalog = catalog;
            this.receiptGenerator = receiptGenerator;
        }

        [HttpPost]
        public ActionResult SetItem(Guid cartId, Guid productId, int quantity, string referrer)
        {
            Cart cart = this.storage.Restore(cartId);
            Product product = this.catalog.Products().First(p => p.Id == productId);
            cart.Set(product, quantity);
            this.storage.Save(cart);

            return this.RedirectToAction("Index", new { cartId, referrer });
        }

        [HttpPost]
        public ActionResult RemoveItem(Guid cartId, Guid itemId, string referrer)
        {
            Cart cart = this.storage.Restore(cartId);
            cart.Remove(itemId);
            this.storage.Save(cart);

            return this.RedirectToAction("Index", new { cartId, referrer });
        }

        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult Index(Guid? cartId, string promo, string referrer)
        {
            Cart cart;
            if (cartId.HasValue)
            {
                cart = this.storage.Restore(cartId.Value);
            }
            else
            {
                cart = new Cart(Guid.NewGuid());
                this.storage.Save(cart);
            }

            cart.Promo = promo;
            cart.Referrer = referrer;

            IEnumerable<Product> products = this.catalog.Products();
            if(String.Equals(this.Request.HttpMethod, "GET", StringComparison.InvariantCultureIgnoreCase))
            {
                return this.View(new StoreViewModel(cart, products));
            }

            Receipt receipt = this.receiptGenerator.Generate(cart);
            return this.View(new StoreViewModel(cart, products, receipt));
        }
    }
}
