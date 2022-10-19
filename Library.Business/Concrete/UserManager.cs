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
            var result = _userRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<User>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<User>(result);
        }

        [CacheAspect]
        public DataResult<List<User>> GetAll()
        {
            var result = _userRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<User>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<User>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        [ValidationAspect(typeof(UserValidator))]
        public DataResult<int> AddAsStudent(User student)
        {
            int result = _userRepository.AddAsStudent(student);
            if (result == 0 || result ==null)
                return new ErrorDataResult<int>("Error occured!");
            return new SuccessDataResult<int>(result, StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IUserService.Get))]
        public Result DeleteFromDb(int id)
        {
            var result = _userRepository.DeleteFromDb(id);
            if (result)
                return new SuccessResult();
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<List<User>> GetDeactiveUsers()
        {
            var result = _userRepository.GetDeactiveUsers();
            return new SuccessDataResult<List<User>>(result);
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
