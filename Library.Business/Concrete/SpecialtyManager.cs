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
    public class SpecialtyManager : ISpecialtyService
    {
        private readonly ISpecialtyRepository _specialtyRepository;
        public SpecialtyManager(ISpecialtyRepository specialtyRepository)
        {
            _specialtyRepository = specialtyRepository;
        }

        [ValidationAspect(typeof(SpecialtyValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISpecialtyService.Get))]
        public Result Add(Specialty value)
        {
            _specialtyRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISpecialtyService.Get))]
        public Result Delete(int id)
        {
            if(_specialtyRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Specialty> Get(int id)
        {
            return new SuccessDataResult<Specialty>(_specialtyRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Specialty>> GetAll()
        {
            return new SuccessDataResult<List<Specialty>>(_specialtyRepository.GetAll());
        }

        [ValidationAspect(typeof(SpecialtyValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISpecialtyService.Get))]
        public Result Update(Specialty value)
        {
            if(_specialtyRepository.Update(value))
                    return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
