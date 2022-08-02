using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class StudentValidator : AbstractValidator<Student>
    {
        public StudentValidator()
        {
        }
    }
}
