using System.ComponentModel;

namespace AddressBookApp.Utilities
{
    public enum ErrorCode
    {
        // 1000s = Data Access
        #region 2000s Object Validations
        [Description("String cannot be null or empty")]
        StringMustNotBeNullOrEmpty = 2001,
        [Description("String length too long")]
        StringLengthTooLong = 2002,
        [Description("Must be a valid email address")]
        EmailValidationFail = 2003,
        [Description("Date must be in the past")]
        DateMustBeInPast = 2004
        #endregion
        // 3000s = Controller
    }
}