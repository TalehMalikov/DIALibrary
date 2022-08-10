using Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
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
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        public Result Delete(int id)
        {
            if (_userRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
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
            if (_userRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
