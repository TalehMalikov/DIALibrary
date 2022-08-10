using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
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
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFacultyService.Get))]
        public Result Delete(int id)
        {
            if(_facultyRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
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
            if(_facultyRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundErrorMessage);
        }
    }
}
