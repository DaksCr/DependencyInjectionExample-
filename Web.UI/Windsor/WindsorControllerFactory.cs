using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Sapiens.Web.UI.Windsor
{
    public class WindsorControllerFactory : DefaultControllerFactory
    {
        private readonly IWindsorContainer container;

        public WindsorControllerFactory(IWindsorContainer container)
        {
            Guard.NotNull(container, "container");
            this.container = container;
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            if (controllerType.IsNull())
            {
                return default(IController);
            }
            return this.container.Resolve(controllerType) as IController;
        }


        public override void ReleaseController(IController controller)
        {
            this.container.Release(controller);
        }
    }
}