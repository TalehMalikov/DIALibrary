using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
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
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Delete(AccountRole value)
        {
            _accountRoleRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<AccountRole> Get(int id)
        {
            return new SuccessDataResult<AccountRole>(_accountRoleRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<AccountRole>> GetAll()
        {
            return new SuccessDataResult<List<AccountRole>>(_accountRoleRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAccountRoleService.Get))]
        public Result Update(AccountRole value)
        {
            _accountRoleRepository.Update(value);
            return new SuccessResult();
        }
    }
}
