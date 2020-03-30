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
    public class ContactValidationDateOfBirthTests : ContactValidationTestBase
    {        
        #region Failure Tests
        [Fact]
        public void Fact_ContactValidation_DoB_CannotBeDefault() =>
            validator.ShouldHaveValidationErrorFor(contact => contact.DateOfBirth, default(DateTime))
            .WithErrorCode(ErrorCode.StringMustNotBeNullOrEmpty.GetErrorCodeString())
            .WithErrorMessage("Invalid Date of Birth: String cannot be null or empty");
            
        [Fact]
        public void Fact_ContactValidation_DoB_CannotBeInPast() =>
            validator.ShouldHaveValidationErrorFor(contact => contact.DateOfBirth, DateTime.Now.AddDays(1))
            .WithErrorCode(ErrorCode.DateMustBeInPast.GetErrorCodeString())
            .WithErrorMessage("Invalid Date of Birth: Date must be in the past");
        #endregion Failure Tests

        #region Success Tests
        [Fact]
        public void Fact_ContactValidation_Success() =>
            validator.ShouldNotHaveValidationErrorFor(contact => contact.DateOfBirth, DateTime.Now.AddYears(-20));
        #endregion Success Tests
    }
}