using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class FacultyManager : IFacultyService
    {
        private readonly IFacultyRepository _facultyRepository;
        public FacultyManager(IFacultyRepository facultyRepository)
        {
            _facultyRepository = facultyRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFacultyService.Get))]
        public Result Add(Faculty value)
        {
            _facultyRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFacultyService.Get))]
        public Result Delete(Faculty value)
        {
            _facultyRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Faculty> Get(int id)
        {
            return new SuccessDataResult<Faculty>(_facultyRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Faculty>> GetAll()
        {
            return new SuccessDataResult<List<Faculty>>(_facultyRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFacultyService.Get))]
        public Result Update(Faculty value)
        {
            _facultyRepository.Update(value);
            return new SuccessResult();
        }
    }
}
