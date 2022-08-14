using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class FileTypeManager : IFileTypeService
    {
        private readonly IFileTypeRepository _fileTypeRepository;
        public FileTypeManager(IFileTypeRepository fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }


        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFileTypeService.Get))]
        public Result Add(FileType value)
        {
            _fileTypeRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFileTypeService.Get))]
        public Result Update(FileType value)
        {
            if (_fileTypeRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFileTypeService.Get))]
        public Result Delete(int id)
        {
            if (_fileTypeRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult();
        }

        public DataResult<FileType> Get(int id)
        {
            var result = _fileTypeRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<FileType>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<FileType>(result);
        }

        public DataResult<List<FileType>> GetAll()
        {
            var result = _fileTypeRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<FileType>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<FileType>>(result);
        }
    }
}
