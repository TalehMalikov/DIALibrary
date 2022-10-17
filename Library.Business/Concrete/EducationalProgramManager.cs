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
    public class EducationalProgramManager : IEducationalProgramService
    {
        private readonly IEducationalProgramRepository _educationalProgram;

        public EducationalProgramManager(IEducationalProgramRepository educationalProgram)
        {
            _educationalProgram = educationalProgram;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IEducationalProgramService.Get))]
        [ValidationAspect(typeof(EducationalProgramValidator))]
        public Result Add(EducationalProgramDto value)
        {
            _educationalProgram.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IEducationalProgramService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Update(EducationalProgramDto value)
        {
            if (_educationalProgram.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IEducationalProgramService.Get))]
        public Result Delete(int id)
        {
            if (_educationalProgram.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<EducationalProgram> Get(int id)
        {
           var result = _educationalProgram.Get(id);
           if (result == null)
               return new ErrorDataResult<EducationalProgram>(result, StatusMessagesUtil.NotFoundMessageGivenId);
           return new SuccessDataResult<EducationalProgram>(result);
        }

        [CacheAspect]
        public DataResult<List<EducationalProgram>> GetAll()
        {
            var result = _educationalProgram.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<EducationalProgram>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<EducationalProgram>>(result);
        }

        [CacheAspect]
        public DataResult<List<EducationalProgram>> GetAllBySpecialtyId(int specialtyId)
        {
            var result = _educationalProgram.GetAllBySpecialtyId(specialtyId);
            if (result.Count == 0)
                return new ErrorDataResult<List<EducationalProgram>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<EducationalProgram>>(result);
        }
    }
}
