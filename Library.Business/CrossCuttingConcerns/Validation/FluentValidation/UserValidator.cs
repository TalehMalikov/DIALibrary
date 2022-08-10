using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => String.IsNullOrWhiteSpace(x.FirstName)).NotEqual(true);
            RuleFor(x => String.IsNullOrWhiteSpace(x.LastName)).NotEqual(true);
            RuleFor(x => String.IsNullOrWhiteSpace(x.FatherName)).NotEqual(true);

            RuleFor(x => x.FirstName).Length(3, 255);
            RuleFor(x => x.LastName).Length(3,255);
            RuleFor(x => x.FatherName).Length(3,255);

            RuleFor(x => x.BirthDate).Empty();
            RuleFor(x => x.BirthDate).GreaterThan(new DateTime(1900, 01, 01));
            RuleFor(x => x.BirthDate).LessThan(DateTime.Now);
            RuleFor(x => x.BirthDate).Must(BeOver18);
        }
        protected bool BeOver18(DateTime date)
        {
            if (DateTime.Now.Year - date.Year >= 18)
                return true;
            return false;
        }
    }
}
