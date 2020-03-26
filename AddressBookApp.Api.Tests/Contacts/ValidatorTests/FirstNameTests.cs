using System;
using AddressBookApp.DataAccess.DomainModels;
using AddressBookApp.DataAccess.Validation;
using AddressBookApp.Utilities;
using FluentValidation.TestHelper;
using Xunit;

namespace AddressBookApp.Tests.Contact.Validation
{
    /// <summary>
    /// Tests focused on logic around validating a Contact.
    /// </summary>
    public class ContactValidationFirstNameTests : ContactValidationTestBase
    {        
        #region Failure tests
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        public void Theory_ContactValidation_FirstName_CannotBeBlank(string firstName) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.FirstName, firstName)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("String cannot be null or empty");

        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")]
        public void Theory_ContactValidation_FirstName_CannotBeTooLong(string firstName) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.FirstName, firstName)
            .WithErrorCode(ErrorCode.StringLengthTooLong.GetErrorCodeString())
            .WithErrorMessage("String length too long");
        #endregion Failure tests

        #region Success Tests
        [Theory]
        [InlineData("John")]
        [InlineData("Heung-min")]
        [InlineData("Heung Min")]
        [InlineData("D'Angelo")]
        public void Theory_ContactValidation_FirstName_Success(string firstName) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.FirstName, firstName);
        #endregion Success Tests
    }
}