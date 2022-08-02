using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class AuthorManager : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorManager(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAuthorService.Get))]
        [ValidationAspect(typeof(AuthorValidator))]
        public Result Add(Author value)
        {
            _authorRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAuthorService.Get))]
        public Result Delete(Author value)
        {
            _authorRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Author> Get(int id)
        {
            return new SuccessDataResult<Author>(_authorRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Author>> GetAll()
        {
            return new SuccessDataResult<List<Author>>(_authorRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAuthorService.Get))]
        [ValidationAspect(typeof(AuthorValidator))]
        public Result Update(Author value)
        {
            _authorRepository.Update(value);
            return new SuccessResult();
        }
    }
}
