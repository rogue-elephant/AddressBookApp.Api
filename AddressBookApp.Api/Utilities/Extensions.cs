using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AddressBookApp.Utilities
{
    public static class Extensions
    {
        /// <summary>
        /// Gets the integer error code from the error code enum, as a string (to be used in validation).
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static string GetErrorCodeString(this ErrorCode code) => ((int)code).ToString();

        /// <summary>
        /// Gets the default message for the error code enum (via the description attribute).
        /// </summary>
        /// <param name="code">The error code to get the message for.</param>
        /// <param name="propertyName">A specific property name to pass in to prepend to the message (Invalid {propertyName}: {message})</param>
        /// <returns></returns>
        public static string GetErrorMessage(this ErrorCode code, string propertyName = null) =>
            $"{(!string.IsNullOrWhiteSpace(propertyName) ? $"Invalid {propertyName}: " : "")}{code.GetDescription()}";

        /// <summary>
        /// Gets the description attribute from the enum value.
        /// </summary>
        /// <param name="enumValue">The specific enum value to look up the attribute on.</param>
        /// <typeparam name="TEnum">The actual enum itself (should be inferred)</typeparam>
        /// <returns></returns>
        public static string GetDescription<TEnum>(this TEnum enumValue) where TEnum: Enum => 
            enumValue.GetType()
                   .GetMember(enumValue.ToString())
                   .First()
                   .GetCustomAttribute<DescriptionAttribute>() ?
                   .Description ?? "";
    }
}