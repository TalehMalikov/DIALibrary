using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class PublicationValidator : AbstractValidator<Publication>
    {
        public PublicationValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.PublisherName)).NotEqual(true);
            RuleFor(x => x.PublisherName).MinimumLength(3);
            RuleFor(x => x.PublisherName).MaximumLength(255);

            RuleFor(x => (int)x.PageNumber).NotNull()
                .GreaterThanOrEqualTo(20)
                .LessThanOrEqualTo(1000);

            RuleFor(x => x.PublicationDate).NotNull()
                .GreaterThan(new DateTime(1500, 1, 1))
                .LessThan(DateTime.Now);

            #region File&Foto -- Dusunurem hele
            RuleFor(x => string.IsNullOrWhiteSpace(x.Photo)).NotEqual(true);
            RuleFor(x => string.IsNullOrWhiteSpace(x.File)).NotEqual(true);
            #endregion

        }
    }
}
