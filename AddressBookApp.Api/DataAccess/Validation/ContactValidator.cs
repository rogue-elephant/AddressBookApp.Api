using System;
using AddressBookApp.Api.DataAccess.DomainModels;
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
                .EmailAddress();

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .LessThan(DateTime.UtcNow);
        }
    }
}