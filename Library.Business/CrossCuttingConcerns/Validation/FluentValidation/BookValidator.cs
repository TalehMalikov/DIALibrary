using FluentValidation;
using Library.Entities.Concrete;

namespace Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class BookValidator : AbstractValidator<Book>
    {
        public BookValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => string.IsNullOrWhiteSpace(p.Name)).NotEqual(true);
            RuleFor(p => p.Name).Length(2, 255);
        }
    }
}
