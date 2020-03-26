using System.ComponentModel;

namespace AddressBookApp.Utilities
{
    /// <summary>
    /// Enumeration of error codes and associated messages.
    /// The codes are divided into blocks of 1000s and 100s for different areas.
    /// </summary>
    public enum ErrorCode
    {
        // 1000s = Data Access
        #region 2000s Object Validations
        #region 10s - generic errors
        [Description("String cannot be null or empty")]
        StringMustNotBeNullOrEmpty = 2001,
        [Description("String length too long")]
        StringLengthTooLong = 2002,
        [Description("Must be a valid email address")]
        EmailValidationFail = 2003,
        [Description("Date must be in the past")]
        DateMustBeInPast = 2004,
        [Description("Must be a valid Date")]
        InvalidDate = 2005
        #endregion 10s - generic errors
        #endregion
        // 3000s = Controller
    }
}