using System.ComponentModel.DataAnnotations;
using System.Resources;

namespace SoccerX.Common.Extensions
{
    public static class ExtensionsCore
    {
        #region Field
        #endregion

        #region Constructor
        #endregion

        #region Public Method
        public static TValue? GetAttributeValue<TAttribute, TValue>(this Type type, Func<TAttribute, TValue> valueSelector) where TAttribute : Attribute
        {
            if (type.GetCustomAttributes(typeof(TAttribute), true).FirstOrDefault() is TAttribute att)
            {
                return valueSelector(att);
            }
            return default;
        }

        /// <summary>
        /// Tries to parse an enum value from a string, returns default if invalid or null.
        /// </summary>
        public static TEnum ToEnum<TEnum>(this string? value, TEnum defaultValue) where TEnum : struct, Enum
        {
            if (!string.IsNullOrWhiteSpace(value) && Enum.TryParse<TEnum>(value, ignoreCase: true, out var parsed))
                return parsed;

            return defaultValue;
        }

        public static bool IsValidEmail(this string email)
        {
            return !string.IsNullOrWhiteSpace(email) && new EmailAddressAttribute().IsValid(email);
        }

        //public static string FromResource(this string resourceKey, ResourceManager resourceManager, params object[] args)
        //{
        //    var resourceString = resourceManager.GetString(resourceKey) ?? $"[{resourceKey}]";

        //    if (args is not { Length: > 0 }) return resourceString;
        //    try
        //    {
        //        return string.Format(resourceString, args);
        //    }
        //    catch (FormatException)
        //    {
        //        // Format hatası durumunda orijinal string'i döndür
        //        return resourceString;
        //    }
        //}

        #endregion

        #region Private Method
        #endregion
    }
}
