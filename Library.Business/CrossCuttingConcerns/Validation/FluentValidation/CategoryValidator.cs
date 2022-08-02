using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class CategoryValidator : AbstractValidator<Category>
    {
        public CategoryValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(2, 255);
        }
    }
}
