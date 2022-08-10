using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

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
        [ValidationAspect(typeof(GroupValidator))]
        public Result Add(Group value)
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
            return new SuccessDataResult<Group>(_groupRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Group>> GetAll()
        {
            return new SuccessDataResult<List<Group>>(_groupRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IGroupService.Get))]
        [ValidationAspect(typeof(GroupValidator))]
        public Result Update(Group value)
        {
            if(_groupRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
