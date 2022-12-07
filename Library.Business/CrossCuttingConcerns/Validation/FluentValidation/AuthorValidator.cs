using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(p => string.IsNullOrWhiteSpace(p.FirstName)).NotEqual(true);
            //RuleFor(p => string.IsNullOrWhiteSpace(p.FatherName)).NotEqual(true);
            RuleFor(p => p.FirstName).MinimumLength(3);
            RuleFor(p => p.LastName).MinimumLength(3);
            //RuleFor(p=> p.FatherName).MinimumLength(3);
            RuleFor(p => p.FirstName).MaximumLength(255);
            RuleFor(p => p.LastName).MaximumLength(255);
            RuleFor(p => p.FatherName).MaximumLength(255);
            //RuleFor(p => p.BookCount).NotEmpty().WithMessage("Number of book count is required");
            //RuleFor(p => (int)p.BookCount).GreaterThanOrEqualTo(1).WithMessage("Number of book count must greater than 30");
        }
    }
}

