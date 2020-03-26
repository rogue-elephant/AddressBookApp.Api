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
        public void Fact_ContactValidation_DoBMustNotBeInPast() =>
            validator.ShouldHaveValidationErrorFor(contact => contact.DateOfBirth, DateTime.Now.AddDays(1));
        #endregion Failure Tests

        #region Success Tests
        #endregion Success Tests
    }
}