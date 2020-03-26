using AddressBookApp.Utilities;
using FluentValidation;

namespace AddressBookApp.DataAccess.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<TEntity, TProperty> AddError<TEntity,TProperty>(this IRuleBuilderOptions<TEntity, TProperty> rule, ErrorCode errorCode)
            => rule
                .WithErrorCode(errorCode.GetErrorCodeString())
                .WithMessage(errorCode.GetErrorMessage());
        public static IRuleBuilderOptions<TEntity, string> NameValidation<TEntity>(this IRuleBuilder<TEntity, string> rule)
        {
            return rule
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty)
                .NotEmpty()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty)
                .MaximumLength(100)
                .AddError(ErrorCode.StringLengthTooLong);
                
                // TODO: Could add regex for ensuring names but this can cause some problems down line... Tekashi 6ix9ine for example....
        }
    }
}