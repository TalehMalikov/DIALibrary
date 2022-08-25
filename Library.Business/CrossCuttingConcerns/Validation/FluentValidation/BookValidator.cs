using FluentValidation;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class BookValidator : AbstractValidator<Entities.Concrete.File>
    {
        public BookValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => string.IsNullOrWhiteSpace(p.Name)).NotEqual(true);
            RuleFor(p => p.Name).Length(2, 255);
        }
    }
}
