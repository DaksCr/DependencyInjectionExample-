using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sapiens
{
    /// <summary>
    /// Contains extension methods of <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes the provided action for each item.
        /// </summary>
        /// <typeparam name="T">A generic parameter that specifies the type of the items.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="action">The action.</param>
        [DebuggerStepThrough]
        public static void ForEach<T>(this IEnumerable<T> instance, Action<T> action)
        {
            Guard.NotNull(action, "action");
            if (instance.IsNotNull())
            {
                foreach (T item in instance)
                {
                    action(item);
                }
            }
        }

        /// <summary>
        /// Determines whether the enumeration is empty or null.
        /// </summary>
        /// <typeparam name="T">A generic parameter that specifies the type of the items.</typeparam>
        /// <param name="instance">The instance.</param>
        /// <returns>true if the enumeration is null or empty; otherwise false.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> instance)
        {
            return instance.IsNull() || !instance.Any();
        }

    }
}
