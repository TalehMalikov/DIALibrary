using Library.Business.Abstraction;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class EducationalProgramManager : IEducationalProgramService
    {
        private readonly IEducationalProgramRepository _educationalProgram;

        public EducationalProgramManager(IEducationalProgramRepository educationalProgram)
        {
            _educationalProgram = educationalProgram;
        }

        public Result Add(EducationalProgram value)
        {
            _educationalProgram.Add(value);
            return new SuccessResult();
        }

        public Result Update(EducationalProgram value)
        {
            if (_educationalProgram.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        public Result Delete(int id)
        {
            if (_educationalProgram.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        public DataResult<EducationalProgram> Get(int id)
        {
           var result = _educationalProgram.Get(id);
           if (result == null)
               return new ErrorDataResult<EducationalProgram>(result, StatusMessagesUtil.NotFoundMessageGivenId);
           return new SuccessDataResult<EducationalProgram>(result);
        }

        public DataResult<List<EducationalProgram>> GetAll()
        {
            var result = _educationalProgram.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<EducationalProgram>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<EducationalProgram>>(result);
        }

        public DataResult<List<EducationalProgram>> GetAllBySpecialtyId(int specialtyId)
        {
            var result = _educationalProgram.GetAllBySpecialtyId(specialtyId);
            if (result.Count == 0)
                return new ErrorDataResult<List<EducationalProgram>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<EducationalProgram>>(result);
        }
    }
}
