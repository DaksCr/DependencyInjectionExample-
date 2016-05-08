using Castle.MicroKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Core;
using Castle.MicroKernel.Context;
using System.Collections.Specialized;

namespace Sapiens.Web.UI.Windsor.Conventions
{
    public class PrimitiveCollectionFromAppSettingsConvention : ISubDependencyResolver
    {
        private readonly NameValueCollection settings;

        public PrimitiveCollectionFromAppSettingsConvention(NameValueCollection settings)
        {
            Guard.NotNull(settings, "settings");
            this.settings = settings;
        }

        public bool CanResolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            return this.settings.AllKeys.Contains(dependency.DependencyKey)
                && dependency.TargetType == typeof(IEnumerable<string>);
        }

        public object Resolve(CreationContext context, ISubDependencyResolver contextHandlerResolver, ComponentModel model, DependencyModel dependency)
        {
            string value = this.settings[dependency.DependencyKey];
            return value.Split(',')
                        .Select(item => item.Trim())
                        .ToList();
        }
    }
}