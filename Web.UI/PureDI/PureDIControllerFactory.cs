using Sapiens.Domain.Adapters;
using Sapiens.Domain.Services;
using Sapiens.Infrastructure;
using Sapiens.Web.Model.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sapiens.Web.UI.PureDI
{
    public class PureDIControllerFactory : DefaultControllerFactory
    {
        private readonly IProductCatalog catalog;
        private readonly ICartStorage storage;

        public PureDIControllerFactory()
        {
            this.storage = new InMemoryCartStorage();
            this.catalog = new InMemoryProductCatalog();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType.IsNull())
            {
                return default(IController);
            }

            if (controllerType == typeof(StoreController))
            {
                IDiscountCalculator discountCalculator = new CompositeDiscountCalculator(new PromoDiscountCalculator(10, "Promo 1", "Promo 2"),
                                                                                         new ReferrerDiscountCalculator(25, "referrer1"));
                ITaxCalculator taxCalculator = new ProductTaxCalculatorDecorator(new SalesTaxCalculator(13), 29, "Producto 1");
                IReceiptGenerator receiptGenerator = new DefaultReceiptGenerator(taxCalculator, discountCalculator);
                return new StoreController(this.storage, this.catalog, receiptGenerator);
            }

            return base.GetControllerInstance(requestContext, controllerType);
        }


        public override void ReleaseController(IController controller)
        {
            base.ReleaseController(controller);
        }
    }
}