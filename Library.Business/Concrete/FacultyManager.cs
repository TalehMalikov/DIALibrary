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
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Faculty> Get(int id)
        {
            var result = _facultyRepository.Get(id);
            return new SuccessDataResult<Faculty>(result);
        }

        [CacheAspect]
        public DataResult<List<Faculty>> GetAll()
        {
            var result = _facultyRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Faculty>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Faculty>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IFacultyService.Get))]
        public Result Update(Faculty value)
        {
            if(_facultyRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
