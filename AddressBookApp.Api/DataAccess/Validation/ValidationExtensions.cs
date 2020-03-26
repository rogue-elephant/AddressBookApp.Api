using AddressBookApp.Utilities;
using FluentValidation;

namespace AddressBookApp.DataAccess.Validation
{
    public static class ValidationExtensions
    {
        /// <summary>
        /// Adds an error code and message to the validation step.
        /// </summary>
        /// <param name="IRuleBuilderOptions<TEntity"></param>
        /// <param name="rule"></param>
        /// <param name="errorCode">The error code to use for the validation step.</param>
        /// <param name="propertyName">The property being validated - user friendly name for displaying in error message.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
        public static IRuleBuilderOptions<TEntity, TProperty> AddError<TEntity,TProperty>(this IRuleBuilderOptions<TEntity, TProperty> rule,
        ErrorCode errorCode, string propertyName = null)
            => rule
                .WithErrorCode(errorCode.GetErrorCodeString())
                .WithMessage(errorCode.GetErrorMessage(propertyName));
        
        /// <summary>
        /// Validates a name property.
        /// </summary>
        /// <param name="IRuleBuilder<TEntity"></param>
        /// <param name="rule"></param>
        /// <param name="propertyName">The property being validated - user friendly name for displaying in error message.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static IRuleBuilderOptions<TEntity, string> NameValidation<TEntity>(this IRuleBuilder<TEntity, string> rule, string propertyName = null)
        {
            return rule
                .NotNullOrEmpty(propertyName)
                .AddError(ErrorCode.StringMustNotBeNullOrEmpty, propertyName)
                .MaximumLength(100)
                .AddError(ErrorCode.StringLengthTooLong, propertyName);
                
                // TODO: Could add regex for ensuring names but this can cause some problems down line... Tekashi 6ix9ine for example....
        }

        /// <summary>
        /// Adds validation to ensure the property is not left blank - null or empty.
        /// </summary>
        /// <param name="IRuleBuilder<TEntity"></param>
        /// <param name="rule"></param>
        /// <param name="propertyName">The property being validated - user friendly name for displaying in error message.</param>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <returns></returns>
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