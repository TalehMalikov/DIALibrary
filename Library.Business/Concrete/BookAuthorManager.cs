using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class BookAuthorManager : IBookAuthorService
    {
        private readonly IBookAuthorRepository _bookAuthorRepository;
        public BookAuthorManager(IBookAuthorRepository bookAuthorRepository)
        {
             _bookAuthorRepository = bookAuthorRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookAuthorService.Get))]
        public Result Add(BookAuthor value)
        {
            _bookAuthorRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookAuthorService.Get))]
        public Result Delete(int id)
        {
            if(_bookAuthorRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<BookAuthor> Get(int id)
        {
            var result = _bookAuthorRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<BookAuthor>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<BookAuthor>(result);
        }

        [CacheAspect]
        public DataResult<List<BookAuthor>> GetAll()
        {
            var result = _bookAuthorRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<BookAuthor>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<BookAuthor>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookAuthorService.Get))]
        public Result Update(BookAuthor value)
        {
            if(_bookAuthorRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
