using Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        [ValidationAspect(typeof(UserValidator))]
        public Result Add(User value)
        {
            _userRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        public Result Delete(User value)
        {
            _userRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<User> Get(int id)
        {
            return new SuccessDataResult<User>(_userRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        [ValidationAspect(typeof(UserValidator))]
        public Result Update(User value)
        {
            _userRepository.Update(value);
            return new SuccessResult();
        }
    }
}
