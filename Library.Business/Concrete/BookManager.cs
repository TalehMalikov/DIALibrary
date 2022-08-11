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
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        public Result Delete(int id)
        {
            if(_bookRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Book> Get(int id)
        {
            var result = _bookRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Book>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Book>(result);
        }

        [CacheAspect]
        public DataResult<List<Book>> GetAll()
        {
            var result = _bookRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Book>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Book>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Update(Book value)
        {
            if(_bookRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
