using FluentValidation;
using Library.Entities.Concrete;

namespace Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => String.IsNullOrWhiteSpace(p.Name)).NotEqual(true);
            RuleFor(p => p.Name).MinimumLength(2);
            RuleFor(p => p.Name).MaximumLength(50);

            RuleFor(p=> p.OriginaLanguage).NotEmpty();
            RuleFor(p=> p.OriginaLanguage.Name).NotEmpty();
            RuleFor(p => String.IsNullOrWhiteSpace(p.OriginaLanguage.Name)).NotEqual(true);
            RuleFor(p => p.OriginaLanguage.Name).MinimumLength(2);
            RuleFor(p => p.OriginaLanguage.Name).MaximumLength(50);

            RuleFor(p=> p.Category).NotEmpty();
            RuleFor(p => p.Category.Name).NotEmpty();
            RuleFor(p => String.IsNullOrWhiteSpace(p.Category.Name)).NotEqual(true);
            RuleFor(p => p.Category.Name).MinimumLength(2);
            RuleFor(p => p.Category.Name).MaximumLength(50);
        }
    }
}
