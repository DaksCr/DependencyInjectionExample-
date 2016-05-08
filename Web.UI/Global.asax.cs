using Castle.Windsor;
using Castle.Windsor.Installer;
using Sapiens.Web.UI.PureDI;
using Sapiens.Web.UI.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sapiens.Web.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private readonly IWindsorContainer container;
        public MvcApplication()
        {
            this.container = new WindsorContainer().Install(FromAssembly.This());
        }
 
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
            //ControllerBuilder.Current.SetControllerFactory(new PureDIControllerFactory());
            ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(this.container));

        }

        private static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Referrer",
                url: "{referrer}",
                defaults: new { controller = "Store", action = "Index" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{referrer}",
                defaults: new { controller = "Store", action = "Index", referrer = UrlParameter.Optional }
            );
        }
    }
}
