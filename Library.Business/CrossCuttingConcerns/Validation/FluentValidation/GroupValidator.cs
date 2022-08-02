using FluentValidation;
using System.Text.RegularExpressions;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class GroupValidator : AbstractValidator<Group>
    {
        public GroupValidator()
        {
            RuleFor(x => String.IsNullOrWhiteSpace(x.Name)).NotEqual(true);
            RuleFor(x => x.Name).Length(3, 6);
        }
    }
}
