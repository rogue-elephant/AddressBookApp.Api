using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace AddressBookApp.Utilities
{
    public static class Extensions
    {
        public static string GetErrorCodeString(this ErrorCode code) => ((int)code).ToString();
        public static string GetErrorMessage(this ErrorCode code, string propertyName = null) =>
            $"{(!string.IsNullOrWhiteSpace(propertyName) ? $"Invalid {propertyName}: " : "")}{code.GetDescription()}";

        public static string GetDescription<T>(this T enumValue) where T: Enum => 
            enumValue.GetType()
                   .GetMember(enumValue.ToString())
                   .First()
                   .GetCustomAttribute<DescriptionAttribute>() ?
                   .Description ?? "";
    }
}