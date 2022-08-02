using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class SpecialtyManager : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        public SpecialtyManager(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        [ValidationAspect(typeof(SpecialtyValidator))]
        [CacheRemoveAspect("Business.Abstract.ISpecialtyService.Get")]
        public Result Add(Speciality value)
        {
            _specialtyRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect("Business.Abstract.ISpecialtyService.Get")]
        public Result Delete(Speciality value)
        {
            _specialtyRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Speciality> Get(int id)
        {
            return new SuccessDataResult<Speciality>(_specialtyRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Speciality>> GetAll()
        {
            return new SuccessDataResult<List<Speciality>>(_specialtyRepository.GetAll());
        }

        [ValidationAspect(typeof(SpecialtyValidator))]
        [CacheRemoveAspect("Business.Abstract.ISpecialtyService.Get")]
        public Result Update(Speciality value)
        {
            _specialtyRepository.Update(value);
            return new SuccessResult();
        }
    }
}
