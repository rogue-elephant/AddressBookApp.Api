using FluentValidation;

namespace AddressBookApp.DataAccess.Validation
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<TEntity, string> NameValidation<TEntity>(this IRuleBuilder<TEntity, string> rule)
        {
            return rule
                .NotEmpty()
                .NotNull()
                .MaximumLength(100);
                
                // TODO: Could add regex for ensuring names but this can cause some problems down line... Tekashi 6ix9ine for example....
        }
    }
}