using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Sapiens.Web.UI.Windsor.Conventions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Sapiens.Web.UI.Windsor.Installers
{
    public class WebWindsorInstaller : IWindsorInstaller
    {
        private static IEnumerable<string> BlackListedNamespaces = new[]
        {
            "Domain.Model",
            "ViewModels",
            "Windsor",
            "PureDI"
        };

        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));
            container.Kernel.Resolver.AddSubResolver(new PrimitiveCollectionFromAppSettingsConvention(ConfigurationManager.AppSettings));
            container.Kernel.ComponentModelBuilder.AddContributor(new PrimitiveFromAppSettingsConvention(ConfigurationManager.AppSettings));
            container.Register(Classes.FromAssembly(KnownAssemblies.WebModel)
                                      .BasedOn(KnownTypes.Controller)
                                      .LifestylePerWebRequest());

            Predicate<AssemblyName> assemblyFilter = assembly => assembly.FullName.StartsWith("Sapiens.");
            Predicate<Type> typeFilter = type => !BlackListedNamespaces.Any(blocked => type.Namespace.Contains(blocked))
                                              && !type.Name.StartsWith("InMemory");
            container.Register(Classes.FromAssemblyInDirectory(new AssemblyFilter("bin").FilterByName(assemblyFilter))
                                      .Where(typeFilter)
                                      .WithServiceAllInterfaces()
                                      .LifestylePerWebRequest());
        }
    }
}