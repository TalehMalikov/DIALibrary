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
        public Result Add(Entities.Concrete.File value)
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
        public DataResult<Entities.Concrete.File> Get(int id)
        {
            var result = _bookRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Entities.Concrete.File>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Entities.Concrete.File>(result);
        }

        [CacheAspect]
        public DataResult<List<Entities.Concrete.File>> GetAll()
        {
            var result = _bookRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Entities.Concrete.File>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Entities.Concrete.File>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Update(Entities.Concrete.File value)
        {
            if(_bookRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
