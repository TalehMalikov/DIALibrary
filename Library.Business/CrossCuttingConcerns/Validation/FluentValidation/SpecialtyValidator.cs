using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class SpecialtyValidator : AbstractValidator<Speciality>
    {
        public SpecialtyValidator()
        {
            RuleFor(x => String.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}
