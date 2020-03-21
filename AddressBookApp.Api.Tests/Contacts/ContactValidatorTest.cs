using System;
using AddressBookApp.Api.DataAccess.DomainModels;
using AddressBookApp.DataAccess.Validation;
using FluentValidation.TestHelper;
using Xunit;

namespace AddressBookApp.Tests
{
    /// <summary>
    /// Tests focused on logic around validating a Contact.
    /// </summary>
    public class ContactValidationTests
    {        
        ContactValidator validator = new ContactValidator();

        #region FirstName
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")] // Cannot be over 100 characters
        public void Theory_ContactValidation_FirstName_Failure(string firstName) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.FirstName, firstName);

        [Theory]
        [InlineData("John")]
        [InlineData("Heung-min")]
        [InlineData("Heung Min")]
        [InlineData("D'Angelo")]
        public void Theory_ContactValidation_FirstName_Success(string firstName) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.FirstName, firstName);
        #endregion FirstName

        #region Surname
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        [InlineData("abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz")] // Cannot be over 100 characters
        public void Theory_ContactValidation_Surname_Failure(string surname) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Surname, surname);

        [Theory]
        [InlineData("Smith")]
        [InlineData("O'Reilly")]
        [InlineData("Van Der Baek")]
        [InlineData("Kobayashi-Maru")]
        public void Theory_ContactValidation_Surname_Success(string surname) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.Surname, surname);
        #endregion Surname

        #region Date of Birth
        [Fact]
        public void Fact_ContactValidation_DoBMustNotBeInPast() =>
            validator.ShouldHaveValidationErrorFor(contact => contact.DateOfBirth, DateTime.Now.AddDays(1));
        #endregion Date of Birth

        #region Email
        [Theory]
        [InlineData(null)] // No nulls
        [InlineData("")] // No empty
        [InlineData("    ")] // No whitespace
        [InlineData("foo")] // need an @
        [InlineData("foo.bar")] // need an @
        [InlineData("foo.bar@")] // need something after @
        [InlineData("@")] // needs more than just @
        [InlineData("@bar")]
        [InlineData("@bar.com")]
        [InlineData(".@.")]
        [InlineData("123455")]
        public void Theory_ContactValidation_Email_Failure(string email) =>
            validator.ShouldHaveValidationErrorFor(contact => contact.Email, email);

        [Theory]
        [InlineData("foo@example.com")]
        [InlineData("Testy.McTestFace@test.com")]
        [InlineData("geocities.isgone@aol.net")]
        [InlineData("the_queen_of_england@royals.co.uk")]
        public void Theory_ContactValidation_Email_Success(string email) =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.Email, email);
        #endregion Email
    }
}