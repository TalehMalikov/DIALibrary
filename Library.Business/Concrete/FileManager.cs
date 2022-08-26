using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using File = Library.Entities.Concrete.File;

namespace Library.Business.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileRepository _fileRepository;
        public FileManager(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        [CacheRemoveAspect(nameof(IFileService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Add(File value)
        {
            _fileRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(IFileService.Get))]
        public Result Delete(int id)
        {
            if(_fileRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<File> Get(int id)
        {
            var result = _fileRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<File>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<File>(result);
        }

        [CacheAspect]
        public DataResult<List<File>> GetAll()
        {
            var result = _fileRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<File>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<File>>(result);
        }

        [CacheAspect]
        public DataResult<List<File>> GetNewAdded()
        {
            var result = _fileRepository.GetNewAdded();
            if (result.Count == 0)
                return new ErrorDataResult<List<File>>(result,StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<File>>(result);
        }

        [CacheAspect]
        public DataResult<List<File>> GetAllFilesByCategoryId(int id)
        {
            var result = _fileRepository.GetAllFilesByCategoryId(id);
            if (result.Count == 0)
                return new ErrorDataResult<List<File>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<File>>(result);
        }

        [CacheRemoveAspect(nameof(IFileService.Get))]
        [ValidationAspect(typeof(BookValidator))]
        public Result Update(File value)
        {
            if(_fileRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
