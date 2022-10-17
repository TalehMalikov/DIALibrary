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
    public class GroupManager : IGroupService
    {
        private readonly IGroupRepository _groupRepository;
        public GroupManager(IGroupRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IGroupService.Get))]
        //[ValidationAspect(typeof(GroupValidator))]
        public Result Add(GroupDto value)
        {
            _groupRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IGroupService.Get))]
        public Result Delete(int id)
        {
            if(_groupRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Group> Get(int id)
        {
            var result = _groupRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Group>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Group>(result);
        }

        [CacheAspect]
        public DataResult<List<Group>> GetAll()
        {
            var result = _groupRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Group>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Group>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IGroupService.Get))]
        //[ValidationAspect(typeof(GroupValidator))]
        public Result Update(GroupDto value)
        {
            if(_groupRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
