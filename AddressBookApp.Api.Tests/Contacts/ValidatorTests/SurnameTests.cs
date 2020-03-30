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
    public class ContactValidationSurnameTests : ContactValidationTestBase
    {      
        #region Failure Tests
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        public void Theory_ContactValidation_Surname_CannotBeBlank(string surname) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Surname, surname)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Surname: String cannot be null or empty");

        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")] // Cannot be over 100 characters
        public void Theory_ContactValidation_Surname_CannotBeTooLong(string surname) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Surname, surname)
            .WithErrorCode(ErrorCode.StringLengthTooLong.GetErrorCodeString())
            .WithErrorMessage("Invalid Surname: String length too long");
        #endregion Failure Tests

        #region Success Tests
        [Theory]
        [InlineData("Smith")]
        [InlineData("O'Reilly")]
        [InlineData("Van Der Baek")]
        [InlineData("Kobayashi-Maru")]
        public void Theory_ContactValidation_Surname_Success(string surname) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.Surname, surname);
        #endregion
    }
}