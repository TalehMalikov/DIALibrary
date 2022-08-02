using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
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
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IStudentService.Get))]
        public Result Delete(Student value)
        {
            _studentRepository.Delete(value.Id);
            return new SuccessResult();
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
            _studentRepository.Update(value);
            return new SuccessResult();
        }
    }
}
