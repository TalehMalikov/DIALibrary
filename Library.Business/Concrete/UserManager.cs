using Core.Aspects.Autofac.Caching;
using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserManager(IUserRepository userRepository)
        {
            userRepository = userRepository;
        }

        [CacheRemoveAspect("Business.Abstract.IAuthorService.Get")]
        [ValidationAspect(typeof(AuthorValidator))]
        public Result Add(User value)
        {
            throw new NotImplementedException();
        }

        public Result Delete(User value)
        {
            throw new NotImplementedException();
        }

        public DataResult<User> Get(int id)
        {
            throw new NotImplementedException();
        }

        public DataResult<List<User>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Result Update(User value)
        {
            throw new NotImplementedException();
        }
    }
}
