using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

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
        public Result Add(SpecialtyDto value)
        {
            _specialtyRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISpecialtyService.Get))]
        public Result Delete(int id)
        {
            if (_specialtyRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Specialty> Get(int id)
        {
            var result = _specialtyRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Specialty>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Specialty>(result);
        }

        [CacheAspect]
        public DataResult<List<Specialty>> GetAll()
        {
            var result = _specialtyRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Specialty>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Specialty>>(result);
        }

        [ValidationAspect(typeof(SpecialtyValidator))]
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ISpecialtyService.Get))]
        public Result Update(SpecialtyDto value)
        {
            if (_specialtyRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
