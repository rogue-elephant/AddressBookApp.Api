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
    public class ContactValidationTestBase
    {        
        protected ContactValidator validator = new ContactValidator();
    }
}