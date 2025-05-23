﻿using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Concrete
{
    public class AccountRoleManager : IAccountRoleService
    {
        private readonly IAccountRoleRepository _accountRoleRepository;
        public AccountRoleManager(IAccountRoleRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Add(AccountRoleDto value)
        {
            _accountRoleRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Delete(int id)
        {
            if (_accountRoleRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<AccountRole> Get(int id)
        {
            var result = _accountRoleRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<AccountRole>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<AccountRole>(result);
        }

        [CacheAspect]
        public DataResult<List<AccountRole>> GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<AccountRole>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<AccountRole>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Update(AccountRoleDto value)
        {
            if(_accountRoleRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<List<Role>> GetRoles()
        {
            var result = _accountRoleRepository.GetRoles();
            if (result.Count == 0)
                return new ErrorDataResult<List<Role>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Role>>(result);
        }
    }
}
