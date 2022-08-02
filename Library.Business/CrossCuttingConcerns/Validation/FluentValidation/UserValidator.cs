using FluentValidation;
using Library.Entities.Concrete;

namespace Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => String.IsNullOrWhiteSpace(x.FirstName)).NotEqual(true);
            RuleFor(x => String.IsNullOrWhiteSpace(x.LastName)).NotEqual(true);
            RuleFor(x => String.IsNullOrWhiteSpace(x.FatherName)).NotEqual(true);

            RuleFor(x => x.FirstName).MinimumLength(3);
            RuleFor(x => x.LastName).MinimumLength(3);
            RuleFor(x => x.FatherName).MinimumLength(3);

            RuleFor(x => x.FirstName).MaximumLength(50);
            RuleFor(x => x.LastName).MaximumLength(50);
            RuleFor(x => x.FatherName).MaximumLength(50);

            RuleFor(x => x.BirthDate).Empty();
            RuleFor(x => x.BirthDate).GreaterThan(new DateTime(1900, 01, 01));
            RuleFor(x => x.BirthDate).LessThan(DateTime.Now);
        }
    }
}
