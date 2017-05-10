﻿using System.ComponentModel;
using System.Reflection;

namespace Infrastructure.CrossCutting.ExtensionMethods
{
    public static class EnumExtensionMethods
    {
        #region "Public"

        public static string GetDescription(this System.Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());
            var attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute),
                false);

            return (attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        #endregion
    }
}
