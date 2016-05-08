#region Libraries
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
#endregion

namespace Sapiens
{
    /// <summary>
    /// Contains extension methods of <see cref="String"/> class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indicates whether a specified string is null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>true if the value is null or <see cref="String.Empty"/>, or if value consists exclusively of white-space characters.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Indicates whether a specified string is not null, empty, or consists only of white-space characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>true if the value is not null or <see cref="String.Empty"/>, or if value consists exclusively of white-space characters.</returns>
        [DebuggerStepThrough]
        public static bool IsNotNullOrEmpty(this string value)
        {
            return !IsNullOrEmpty(value);
        }

        /// <summary>
        /// Replaces the format item in the string with the string representation of a corresponding object in a specified array.
        /// </summary>
        /// <param name="format">The string.</param>
        /// <param name="args">An object array that contains zero or more objects to format.</param>
        /// <returns> A copy of format in which the format items have been replaced by the string representation of the corresponding objects in args.</returns>
        [DebuggerStepThrough]
        public static string FormatWith(this string format, params object[] args)
        {
            Guard.NotNullOrEmpty(format, "format");

            return String.Format(format, args);
        }

        /// <summary>
        /// Indicates whether the string is a number or not.
        /// </summary>
        /// <param name="value">The string.</param>
        /// <returns>true if the string is a number; otherwise, false.</returns>
        [DebuggerStepThrough]
        public static bool IsNumeric(this string value)
        {
            double output;
            return Double.TryParse(value, out output);
        }

        /// <summary>
        /// Truncates the string with the given length.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <param name="length">The length of the output.</param>
        /// <returns>A string trucated in the given length.</returns>
        [DebuggerStepThrough]
        public static string Truncate(this string @string, int length)
        {
            if (@string.IsNullOrEmpty())
            {
                return String.Empty;
            }

            return (@string.Length > length)
                 ? String.Concat(@string.Substring(0, length), "...")
                 : @string;
        }

        /// <summary>
        /// Returns a value indicating whether the specified System.String object occurs within this string.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparison">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string; otherwise, false.</returns>
        [DebuggerStepThrough]
        public static bool Contains(this string @string, string value, StringComparison comparison)
        {
            return @string.IndexOf(value, comparison) >= 0;
        }

        /// <summary>
        /// Creates a URL friendly slug from a string
        /// </summary>
        [DebuggerStepThrough]
        public static string ToUrlSlug(this string value)
        {
            //TODO: Create a strategy to generate the slug, so the strategy can be changed whenever is needed.
            string originalValue = value;

            // Repalce any characters that are not alphanumeric with hypen
            value = Regex.Replace(value, "[^a-z^0-9]", "-", RegexOptions.IgnoreCase);

            // Replace all double hypens with single hypen
            string pattern = "--";
            while (Regex.IsMatch(value, pattern))
                value = Regex.Replace(value, pattern, "-", RegexOptions.IgnoreCase);

            // Remove leading and trailing hypens ("-")
            pattern = "^-|-$";
            value = Regex.Replace(value, pattern, "", RegexOptions.IgnoreCase);

            return value.ToLower();
        }

        /// <summary>
        /// Capitalize the string.
        /// </summary>
        /// <param name="string">The string.</param>
        /// <returns>The string capitalized.</returns>
        [DebuggerStepThrough]
        public static string Capitalize(this string @string)
        {
            if (@string.IsNullOrEmpty())
            {
                return String.Empty;
            }

            if (@string.Length == 1)
            {
                return @string.ToUpperInvariant();
            }

            return String.Concat(@string.Substring(0, 1).ToUpperInvariant(), @string.Substring(1));
        }
    }
}
