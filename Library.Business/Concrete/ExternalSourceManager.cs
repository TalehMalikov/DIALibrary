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
    public class ExternalSourceManager : IExternalSourceService
    {
        private readonly IExternalSourceRepository _externalSourceRepository;
        public ExternalSourceManager(IExternalSourceRepository externalSourceRepository)
        {
            _externalSourceRepository = externalSourceRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IExternalSourceService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Add(ExternalSource value)
        {
            _externalSourceRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IExternalSourceService.Get))]
        public Result Delete(int id)
        {
            if (_externalSourceRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<ExternalSource> Get(int id)
        {
            var result = _externalSourceRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<ExternalSource>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<ExternalSource>(result);
        }

        [CacheAspect]
        public DataResult<List<ExternalSource>> GetAll()
        {
            var result = _externalSourceRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<ExternalSource>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<ExternalSource>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IExternalSourceService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Update(ExternalSource value)
        {
            if (_externalSourceRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
