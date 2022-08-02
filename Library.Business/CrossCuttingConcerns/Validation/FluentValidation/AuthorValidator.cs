using FluentValidation;
using Library.Entities.Concrete;
using Library.Business.Extensions;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class AuthorValidator : AbstractValidator<Author>
    {
        public AuthorValidator()
        {
            RuleFor(p => string.IsNullOrWhiteSpace(p.FirstName)).NotEqual(true).WithMessage("Firstname is required. It mustn't be empty or white space");
            RuleFor(p => string.IsNullOrWhiteSpace(p.LastName)).NotEqual(true).WithMessage("Lastname is required. It mustn't be empty or white space");
            RuleFor(p => string.IsNullOrWhiteSpace(p.FatherName)).NotEqual(true).WithMessage("Fathername is required. It mustn't be empty or white space");
            RuleFor(p => p.FirstName).MinimumLength(3).WithMessage("Min. 3 number of characters for Firstname is required");
            //RuleFor(p => p.FirstName).MinimumLength(3).MinimumLengthMessage(3);
            RuleFor(p => p.LastName).MinimumLength(3).WithMessage("Min. 3 number of characters for Lastname is required");
            RuleFor(p=> p.FatherName).MinimumLength(3).WithMessage("Min. 3 number of characters for Fathername is required");
            RuleFor(p => p.FirstName).MaximumLength(50).WithMessage("Max. 50 number of characters for Firstname is required");
            RuleFor(p => p.LastName).MaximumLength(50).WithMessage("Max. 50 number of characters for Lastname is required");
            RuleFor(p => p.FatherName).MaximumLength(50).WithMessage("Max. 50 number of characters for Fathername is required");
            RuleFor(p => p.BookCount).NotEmpty().WithMessage("Number of book count is required");
            RuleFor(p => (int)p.BookCount).GreaterThan(30).WithMessage("Number of book count must greater than 30");
            RuleFor(p => (int)p.BookCount).LessThan(1000).WithMessage("Number of book count must less than 100");
        }
    }
}
