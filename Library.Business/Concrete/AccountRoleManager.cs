using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

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
        public Result Add(AccountRole value)
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
            return new SuccessDataResult<List<AccountRole>>();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Update(AccountRole value)
        {
            if(_accountRoleRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
