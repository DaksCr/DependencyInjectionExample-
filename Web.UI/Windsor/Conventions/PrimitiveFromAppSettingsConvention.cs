using Castle.MicroKernel.ModelBuilder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.Core;
using Castle.MicroKernel;
using System.Collections.Specialized;

namespace Sapiens.Web.UI.Windsor.Conventions
{
    public class PrimitiveFromAppSettingsConvention : IContributeComponentModelConstruction
    {
        private readonly NameValueCollection settings;

        public PrimitiveFromAppSettingsConvention(NameValueCollection settings)
        {
            Guard.NotNull(settings, "settings");
            this.settings = settings;
        }

        public void ProcessModel(IKernel kernel, ComponentModel model)
        {
            model.Dependencies
                 .Where(dependency => dependency.IsPrimitiveTypeDependency
                                   && this.settings.AllKeys.Contains(dependency.DependencyKey)
                                   && !model.Parameters.Any(param => param.Name == dependency.DependencyKey))
                 .ForEach(dependency => model.Parameters.Add(dependency.DependencyKey, this.settings[dependency.DependencyKey]));
        }

        private bool IsPrimitiveAndFoundInSettings(DependencyModel dependency)
        {
            return dependency.IsPrimitiveTypeDependency
                && this.settings.AllKeys.Contains(dependency.DependencyKey);
        }
    }
}