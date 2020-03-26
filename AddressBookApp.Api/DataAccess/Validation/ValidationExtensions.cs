using AddressBookApp.Utilities;
using FluentValidation;

namespace AddressBookApp.DataAccess.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<TEntity, TProperty> AddError<TEntity,TProperty>(this IRuleBuilderOptions<TEntity, TProperty> rule,
        ErrorCode errorCode, string propertyName = null)
            => rule
                .WithErrorCode(errorCode.GetErrorCodeString())
                .WithMessage(errorCode.GetErrorMessage(propertyName));
        public static IRuleBuilderOptions<TEntity, string> NameValidation<TEntity>(this IRuleBuilder<TEntity, string> rule, string propertyName = null)
        {
            return rule
                .NotNullOrEmpty(propertyName)
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, propertyName)
                .MaximumLength(100)
                .AddError(ErrorCode.StringLengthTooLong, propertyName);
                
                // TODO: Could add regex for ensuring names but this can cause some problems down line... Tekashi 6ix9ine for example....
        }
         public static IRuleBuilderOptions<TEntity, TProperty> NotNullOrEmpty<TEntity,TProperty>(this IRuleBuilder<TEntity, TProperty> rule,
         string propertyName = null)
        {
            return rule
                .NotNull()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, propertyName)
                .NotEmpty()
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, propertyName);
        }
    }
}