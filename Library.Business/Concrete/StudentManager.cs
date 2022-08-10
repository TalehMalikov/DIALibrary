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
            _studentRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Delete(int id)
        {
            if(_studentRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
        }
        
        [CacheAspect]
        public DataResult<Student> Get(int id)
        {
            return new SuccessDataResult<Student>(_studentRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Student>> GetAll()
        {
            return new SuccessDataResult<List<Student>>(_studentRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Update(Student value)
        {
            if(_studentRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
        }
    }
}
