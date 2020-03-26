using System;
using AddressBookApp.DataAccess.DomainModels;
using AddressBookApp.Utilities;
using FluentValidation;

namespace AddressBookApp.DataAccess.Validation
{
    /// <summary>
    /// Provides validation rules for the contact domain model
    /// </summary>
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NameValidation("First Name");

            RuleFor(x => x.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NameValidation("Surname");

            RuleFor(x => x.Email)
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, "Email")
                .EmailAddress()
                .AddError(ErrorCode.EmailValidationFail, "Email");

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, "Date of Birth")
                .LessThan(DateTime.UtcNow)
                .AddError(ErrorCode.DateMustBeInPast, "Date of Birth");
        }
    }
}