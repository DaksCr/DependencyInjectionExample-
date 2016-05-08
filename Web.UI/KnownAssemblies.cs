using Sapiens.Infrastructure;
using Sapiens.Web.Model.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Sapiens.Web.UI
{
    internal static class KnownAssemblies
    {
        public static Assembly WebModel { get { return typeof(StoreController).Assembly; } }
        public static Assembly Infrastructure { get { return typeof(InMemoryCartStorage).Assembly; } }
    }
}