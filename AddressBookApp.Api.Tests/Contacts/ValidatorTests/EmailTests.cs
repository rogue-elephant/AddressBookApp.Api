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
    public class ContactValidationEmailTests : ContactValidationTestBase
    {        
        #region Failure Tests
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        public void Theory_ContactValidation_Email_CannotBeBlank(string email) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Email, email)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Email: String cannot be null or empty");

        [InlineData("foo")] // need an @
        [InlineData("foo.bar")] // need an @
        [InlineData("foo.bar@")] // need something after @
        [InlineData("@")] // needs more than just @
        [InlineData("@bar")]
        [InlineData("@bar.com")]
        [InlineData(".@.")]
        [InlineData("123455")]
        public void Theory_ContactValidation_Email_MustBeValidEmail(string email) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Email, email)
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Email: Must be a valid Email");
        #endregion Failure Tests

        #region Success Tests
        [Theory]
        [InlineData("foo@example.com")]
        [InlineData("Testy.McTestFace@test.com")]
        [InlineData("geocities.isgone@aol.net")]
        [InlineData("the_queen_of_england@royals.co.uk")]
        public void Theory_ContactValidation_Email_Success(string email) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.Email, email);
        #endregion
    }
}