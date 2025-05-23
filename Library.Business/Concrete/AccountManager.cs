﻿using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

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
            var result = _accountRepository.GetByEmail(email);
            if (result == null)
                return new ErrorDataResult<Account>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<Account>(result);
        }

        public DataResult<Account> GetByAccountName(string accountName)
        {
            var result = _accountRepository.GetByAccountName(accountName);
            if (result == null)
                return new ErrorDataResult<Account>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<Account>(result);
        }

        [CacheAspect]
        public DataResult<List<Role>> GetRoles(Account account)
        {
            return new SuccessDataResult<List<Role>>(_accountRepository.GetRoles(account));
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        [ValidationAspect(typeof(AccountValidator))]
        public DataResult<int> Add(AccountDto value)
        {
            var result = _accountRepository.Add(value);
            if (result != 0)
                return new SuccessDataResult<int>(result,StatusMessagesUtil.AddSuccessMessage);
            return new ErrorDataResult<int>();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        public Result Delete(int id)
        {
            if (_accountRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Account> Get(int id)
        {
            var result = _accountRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Account>(result,StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Account>(result);
        }

        [CacheAspect]
        public DataResult<List<Account>> GetAll()
        {
            var result = _accountRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Account>>(result,StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Account>>(result);
        }
        
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountService.Get))]
        [ValidationAspect(typeof(AccountValidator))]
        public Result Update(AccountDto value)
        {
            if (_accountRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
