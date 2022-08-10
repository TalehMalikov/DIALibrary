using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class AccountManager : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [CacheAspect]
        public DataResult<Account> GetByEmail(string email)
        {
            return new SuccessDataResult<Account>(_accountRepository.GetByEmail(email));
        }

        [CacheAspect]
        public DataResult<List<Role>> GetRoles(Account account)
        {
            return new SuccessDataResult<List<Role>>(_accountRepository.GetRoles(account));
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        [ValidationAspect(typeof(AccountValidator))]
        public Result Add(Account value)
        {
            _accountRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        public Result Delete(int id)
        {
            if (!_accountRepository.Delete(id))
                return new ErrorResult(StatusMessagesUtil.DeleteErrorMessage);
            return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
        }

        [CacheAspect]
        public DataResult<Account> Get(int id)
        {
            return new SuccessDataResult<Account>(_accountRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Account>> GetAll()
        {
            return new SuccessDataResult<List<Account>>(_accountRepository.GetAll());
        }
        
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        [ValidationAspect(typeof(AccountValidator))]
        public Result Update(Account value)
        {
            _accountRepository.Update(value);
            return new SuccessResult();
        }
    }
}
