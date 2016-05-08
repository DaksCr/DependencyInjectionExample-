#region Libraries
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#endregion

namespace Sapiens
{
    /// <summary>
    /// Contains different guard clauses to validate arguments and protect a piece of code invariants.
    /// Really useful to write defensive programming code.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Guard clause to prevent null arguments, if the provided parameter value is null an exception is thrown.
        /// </summary>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void NotNull(object parameter, string parameterName, string message = null)
        {
            if (parameter.IsNull())
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentNullException(parameterName)
                    : new ArgumentNullException(parameterName, message);
            }
        }

        /// <summary>
        /// Guard clause to prevent <see cref="String"/> values to be null, empty or contains only white spaces.
        /// </summary>
        /// <param name="parameter">The string value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNullOrEmpty(string parameter, string parameterName, string message = null)
        {
            if (parameter.IsNullOrEmpty())
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentException("{0} cannot be null or empty".FormatWith(parameterName))
                    : new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard cluase to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotDefault<TValue>(TValue parameter, string parameterName, string message = null)
        {
            if (parameter.Equals(default(TValue)))
            {
                throw message.IsNullOrEmpty()
                    ? new ArgumentException("{0} cannot be the default value".FormatWith(parameterName))
                    : new ArgumentException(message, parameterName);
            }
        }

        /// <summary>
        /// Guard cluase to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNegativeOrZero(int parameter, string parameterName, string message = null)
        {
            if (parameter <= 0)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException("{0} cannot be negative or zero".FormatWith(parameterName))
                        : new ArgumentOutOfRangeException(message, parameterName);
            }

        }

        /// <summary>
        /// Guard cluase to prevent a value to be its default value. <remarks>default(TValue)</remarks>
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="parameter">The value.</param>
        /// <param name="parameterName">The parameter's name.</param>
        /// <param name="message">A custom message.</param>
        /// <exception cref="ArgumentException"></exception>
        public static void NotNegativeOrZero(decimal parameter, string parameterName, string message = null)
        {
            if (parameter <= 0)
            {
                throw message.IsNullOrEmpty()
                        ? new ArgumentOutOfRangeException("{0} cannot be negative or zero".FormatWith(parameterName))
                        : new ArgumentOutOfRangeException(message, parameterName);
            }

        }
    }
}
