using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class EducationalProgramValidator : AbstractValidator<EducationalProgram>
    {
        public EducationalProgramValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
        }
    }
}
