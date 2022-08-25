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
    public class PublicationManager : IPublicationService
    {
        private readonly IPublicationRepository _publicationRepository;
        public PublicationManager(IPublicationRepository publicationRepository)
        {
            _publicationRepository = publicationRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IPublicationService.Get))]
        [ValidationAspect(typeof(PublicationValidator))]
        public Result Add(Publication value)
        {
            _publicationRepository.Add(value);
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IPublicationService.Get))]
        public Result Delete(int id)
        {
            if (_publicationRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Publication> Get(int id)
        {
            var result = _publicationRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Publication>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Publication>(result);
        }

        [CacheAspect]
        public DataResult<List<Publication>> GetAll()
        {
            var result = _publicationRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Publication>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Publication>>(result);
        }

        public DataResult<List<Publication>> GetNewAdded()
        {
            var result = _publicationRepository.GetNewAdded();
            if (result.Count == 0)
                return new ErrorDataResult<List<Publication>>(result, StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Publication>>(result);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IPublicationService.Get))]
        [ValidationAspect(typeof(PublicationValidator))]
        public Result Update(Publication value)
        {
            if (_publicationRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
