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
    public class SectorManager : ISectorService
    {
        private readonly ISectorRepository _sectorRepository;
        public SectorManager(ISectorRepository sectorRepository)
        {
            _sectorRepository = sectorRepository;
        }

        [ValidationAspect(typeof(SectorValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISectorService.Get))]
        public Result Add(Sector value)
        {
            _sectorRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISectorService.Get))]
        public Result Delete(int id)
        {
            if(_sectorRepository.Delete(id))
                   return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Sector> Get(int id)
        {
            var result = _sectorRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Sector>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Sector>(result);
        }
        
        [CacheAspect]
        public DataResult<List<Sector>> GetAll()
        {
            var result = _sectorRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Sector>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Sector>>(result);
        }

        [ValidationAspect(typeof(SectorValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISectorService.Get))]
        public Result Update(Sector value)
        {
            if(_sectorRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
