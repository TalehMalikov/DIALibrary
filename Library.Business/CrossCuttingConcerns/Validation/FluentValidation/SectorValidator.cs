using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class SectorValidator : AbstractValidator<Sector>
    {
        public SectorValidator()
        {
            RuleFor(x => String.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(3, 50);
        }
    }
}
