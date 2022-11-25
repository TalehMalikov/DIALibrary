using Library.Business.Abstraction;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class ActivityManager : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        public ActivityManager(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [CacheRemoveAspect(nameof(IActivityService.Get))]
        public Result Add(Activity value)
        {
            _activityRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(IActivityService.Get))]
        public Result Update(Activity value)
        {
            if (_activityRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheRemoveAspect(nameof(IActivityService.Get))]
        public Result DeleteFromDb(int id)
        {
            if (_activityRepository.DeleteFromDb(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheRemoveAspect(nameof(IActivityService.Get))]
        public Result Activate(int id)
        {
            var result = _activityRepository.Activate(id);
            if (result)
                return new SuccessResult();
            return new ErrorResult();
        }

        public DataResult<List<Activity>> GetDeletedActivities()
        {
            var result = _activityRepository.GetDeletedActivities();
            if (result.Count == 0)
                return new ErrorDataResult<List<Activity>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Activity>>(result);
        }

        [CacheRemoveAspect(nameof(IActivityService.Get))]
        public Result Delete(int id)
        {
            if (_activityRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Activity> Get(int id)
        {
            var result = _activityRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Activity>(result,StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Activity>(result);
        }

        [CacheAspect]
        public DataResult<List<Activity>> GetAll()
        {
            var result = _activityRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Activity>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Activity>>(result);
        }
    }
}
