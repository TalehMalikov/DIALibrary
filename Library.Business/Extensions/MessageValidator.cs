using FluentValidation;
using Library.Entities.Abstract;
using Library.Entities.Concrete;

namespace Library.Business.Extensions
{
    public static class MessageValidator
    {
        public static IRuleBuilderOptions<BaseEntity, string> MinimumLengthMessage(this IRuleBuilderOptions<BaseEntity, string> rule, int count)
        {
            return rule.WithMessage($"Min. {count} number of characters for Firstname is required");
        }
    }
}
