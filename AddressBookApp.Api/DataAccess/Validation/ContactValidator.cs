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
                .NameValidation();

            RuleFor(x => x.Surname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                .NameValidation();

            RuleFor(x => x.Email)
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty)
                .EmailAddress()
                .AddError(ErrorCode.EmailValidationFail);

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty)
                .LessThan(DateTime.UtcNow)
                .AddError(ErrorCode.DateMustBeInPast);
        }
    }
}