using FluentValidation;
using File = Library.Entities.Concrete.File;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class FileValidator : AbstractValidator<File>
    {
        public FileValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(3, 50);
            RuleFor(x => string.IsNullOrWhiteSpace(x.PhotoPath)).NotEqual(true);
            RuleFor(x => string.IsNullOrWhiteSpace(x.FilePath)).NotEqual(true);
            RuleFor(x => string.IsNullOrWhiteSpace(x.PublisherName)).NotEqual(true);
            RuleFor(x => string.IsNullOrWhiteSpace(x.Description)).NotEqual(true);
            RuleFor(x => x.Description).Length(3, 100);
            RuleFor(x => x.PublisherName).Length(3, 100);
        }
    }
}
