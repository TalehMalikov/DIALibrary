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
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
        }

        [CacheAspect]
        public DataResult<BookAuthor> Get(int id)
        {
            return new SuccessDataResult<BookAuthor>(_bookAuthorRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<BookAuthor>> GetAll()
        {
            return new SuccessDataResult<List<BookAuthor>>(_bookAuthorRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookAuthorService.Get))]
        public Result Update(BookAuthor value)
        {
            if(_bookAuthorRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
        }
    }
}
