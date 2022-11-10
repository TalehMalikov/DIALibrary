using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Concrete
{
    public class BookAuthorManager : IBookAuthorService
    {
        private readonly IFileAuthorRepository _fileAuthorRepository;
        public BookAuthorManager(IFileAuthorRepository fileAuthorRepository)
        {
             _fileAuthorRepository = fileAuthorRepository;
        }


        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IBookAuthorService.Get))]
        public Result Delete(int id)
        {
            if(_fileAuthorRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<BookAuthor> Get(int id)
        {
            var result = _fileAuthorRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<BookAuthor>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<BookAuthor>(result);
        }

        [CacheAspect]
        public DataResult<List<BookAuthor>> GetAll()
        {
            var result = _fileAuthorRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<BookAuthor>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<BookAuthor>>(result);
        }

        [CacheAspect]
        public Result AddAllFilesAuthor(List<int> authorIds,int fileId)
        {
            var result = _fileAuthorRepository.AddAllFilesAuthor(authorIds,fileId);
            if (result)
                return new SuccessResult(StatusMessagesUtil.NotFoundMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IAuthorService.Get))]
        public Result DeleteFileAuthor(int fileId)
        {
            if (_fileAuthorRepository.DeleteFileAuthor(fileId))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<List<int>> GetAllFileAuthors(int fileId)
        {
            var result = _fileAuthorRepository.GetAllFileAuthors(fileId);
            if (result.Count == 0)
                return new ErrorDataResult<List<int>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<int>>(result);
        }
    }
}
