using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
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
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IPublicationService.Get))]
        public Result Delete(Publication value)
        {
            _publicationRepository.Delete(value.Id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Publication> Get(int id)
        {
            return new SuccessDataResult<Publication>(_publicationRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Publication>> GetAll()
        {
            return new SuccessDataResult<List<Publication>>(_publicationRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.IPublicationService.Get))]
        [ValidationAspect(typeof(PublicationValidator))]
        public Result Update(Publication value)
        {
            _publicationRepository.Update(value);
            return new SuccessResult();
        }
    }
}
