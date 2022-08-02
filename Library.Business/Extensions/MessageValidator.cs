using FluentValidation;

namespace Library.Business.Extensions
{
    public static class MessageValidator
    {
        public static IRuleBuilderOptions<T, string> MinLengthMessage<T>(this IRuleBuilderOptions<T, string> rule, int count, string propertyName)
        {
            return rule.WithMessage($"Min. {count} number of characters for {propertyName} is required");
        }
        public static IRuleBuilderOptions<T, string> MaxLengthMessage<T>(this IRuleBuilderOptions<T, string> rule, int count, string propertyName)
        {
            return rule.WithMessage($"Max. {count} number of characters for {propertyName} is required");
        }
    }
}    
