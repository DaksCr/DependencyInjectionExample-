using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace Sapiens.Web.UI.Windsor.Installers
{
    public class InMemoryServicesWindsorInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            Predicate<Type> typeFilter = type => type.Name.StartsWith("InMemory");
            container.Register(Classes.FromAssembly(KnownAssemblies.Infrastructure)
                                      .Where(typeFilter)
                                      .WithServiceAllInterfaces()
                                      .LifestyleSingleton());
        }
    }
}