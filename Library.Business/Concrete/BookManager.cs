using Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class BookManager : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookManager(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Add(Book value)
        {
            _bookRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        public Result Delete(int id)
        {
            _bookRepository.Delete(id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Book> Get(int id)
        {
            return new SuccessDataResult<Book>(_bookRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Book>> GetAll()
        {
            return new SuccessDataResult<List<Book>>(_bookRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Update(Book value)
        {
            _bookRepository.Update(value);
            return new SuccessResult();
        }
    }
}
