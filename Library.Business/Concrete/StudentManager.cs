using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class StudentManager : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        public StudentManager(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        
        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Add(Student value)
        {
            var result = _studentRepository.Add(value);
            if (!result)
                return new ErrorResult();
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Delete(int id)
        {
            if(_studentRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
        
        [CacheAspect]
        public DataResult<Student> Get(int id)
        {
            var result = _studentRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Student>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Student>(result);
        }

        [CacheAspect]
        public DataResult<List<Student>> GetAll()
        {
            var result = _studentRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Student>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Student>>(result);
        }

        public DataResult<Student> GetByUserId(int id)
        {
            var result = _studentRepository.GetByUserId(id);
            if (result == null)
                return new ErrorDataResult<Student>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Student>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Update(Student value)
        {
            if(_studentRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
