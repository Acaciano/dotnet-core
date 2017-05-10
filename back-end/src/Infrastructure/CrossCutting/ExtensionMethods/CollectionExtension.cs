using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.CrossCutting.ExtensionMethods
{
    public static class CollectionExtension
    {
        /// <summary>
        ///     Try if any object's List is null or empty
        /// </summary>
        /// <typeparam name="T">Any object</typeparam>
        /// <param name="list">List</param>
        /// <returns></returns>
        public static bool IsNullOrEmptyList<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static string GetExceptionMessage(this Exception ex)
        {
            if (ex == null)
            {
                return "Exception error undefined";
            }

            return ex.InnerException != null ? ex.InnerException.Message : ex.Message;
        }
    }

    public static class RuleExtension
    {
        /// <summary>
        ///     Round value with role (from 1.5 = 2 or 1.49 = 1)
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static decimal RoundValue(this decimal value)
        {
            if (value == 0)
                return 0;

            return Math.Round(Math.Floor(value + .5M), 2);
        }

        public static decimal TrimValue(this decimal value, int decimalPlaces = 2)
        {
            if (value == 0)
                return 0;

            return Math.Round(value, decimalPlaces);
        }

        public static decimal TruncateValue(this decimal value)
        {
            return decimal.Truncate(value * 100) / 100;
        }
    }
}
