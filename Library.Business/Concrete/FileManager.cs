using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.Business.Concrete
{
    public class FileManager : IFileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IFileAuthorRepository _fileAuthorRepository;
        public FileManager(IFileRepository fileRepository, IFileAuthorRepository fileAuthorRepository)
        {
            _fileRepository = fileRepository;
            _fileAuthorRepository = fileAuthorRepository;
        }

        [CacheRemoveAspect(nameof(IFileService.Get))]
        //[ValidationAspect(typeof(BookValidator))]
        public DataResult<int> Add(FileDto value)
        {
            int id = _fileRepository.Add(value);
            return new SuccessDataResult<int>(id,StatusMessagesUtil.AddSuccessMessage);
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
        [CacheAspect]
        public DataResult<FileAuthorDto> GetFileWithAuthors(int fileId)
        {
            var result = _fileAuthorRepository.GetFileWithAuthors(fileId);
            result.File = Get(fileId).Data;
            if (result == null)
                return new ErrorDataResult<FileAuthorDto>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<FileAuthorDto>(result);
        }
        [CacheAspect]
        public DataResult<List<FileAuthorDto>> GetAllFilesWithAuthors()
        {
            var result = _fileAuthorRepository.GetAllFilesWithAuthors(GetAll().Data);
            if (result.Count == 0)
                return new ErrorDataResult<List<FileAuthorDto>>(result,StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<FileAuthorDto>>(result);
        }

        [CacheRemoveAspect(nameof(IFileService.Get))]
        //[ValidationAspect(typeof(BookValidator))]
        public Result Update(FileDto value)
        {
            if(_fileRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
