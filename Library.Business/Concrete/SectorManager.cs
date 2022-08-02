using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
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
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISectorService.Get))]
        public Result Delete(Sector value)
        {
            _sectorRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Sector> Get(int id)
        {
            return new SuccessDataResult<Sector>(_sectorRepository.Get(id));
        }
        
        [CacheAspect]
        public DataResult<List<Sector>> GetAll()
        {
            return new SuccessDataResult<List<Sector>>(_sectorRepository.GetAll());
        }

        [ValidationAspect(typeof(SectorValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISectorService.Get))]
        public Result Update(Sector value)
        {
            _sectorRepository.Update(value);
            return new SuccessResult();
        }
    }
}
