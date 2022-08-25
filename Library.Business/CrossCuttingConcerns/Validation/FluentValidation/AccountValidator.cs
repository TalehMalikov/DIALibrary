using FluentValidation;
using Library.Entities.Concrete;

namespace Library.Business.CrossCuttingConcerns.Validation.FluentValidation
{
    public class AccountValidator : AbstractValidator<Account>
    {
        public AccountValidator()
        {
            RuleFor(x => string.IsNullOrWhiteSpace(x.AccountName)).NotEqual(true);
            RuleFor(account => account.AccountName).NotEmpty()
                .Length(10, 50)
                .Must((account, accountName) => accountName.Contains(account.User.FirstName))
                .Must((account, accountName) => accountName.Contains(account.User.LastName));

            RuleFor(x => x.Email).EmailAddress();
        }
    }
}


//RuleFor(customer => customer.Address.Postcode).NotNull().When(customer => customer.Address != null)

/*
 RuleFor(customer => customer.Photo)
    .NotEmpty()
    .Matches("https://wwww.photos.io/\d+\.png")
    .When(customer => customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator)
    .Empty()
    .When(customer => ! customer.IsPreferredCustomer, ApplyConditionTo.CurrentValidator);
 */

// 0-255

