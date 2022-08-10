using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class LanguageManager : ILanguageService
    {
        private readonly ILanguageRepository _languageRepository;
        public LanguageManager(ILanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ILanguageService.Get))]
        [ValidationAspect(typeof(LanguageValidator))]
        public Result Add(Language value)
        {
            _languageRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ILanguageService.Get))]
        public Result Delete(int id)
        {
            _languageRepository.Delete(id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Language> Get(int id)
        {
            return new SuccessDataResult<Language>(_languageRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Language>> GetAll()
        {
            return new SuccessDataResult<List<Language>>(_languageRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ILanguageService.Get))]
        [ValidationAspect(typeof(LanguageValidator))]
        public Result Update(Language value)
        {
            _languageRepository.Update(value);
            return new SuccessResult();
        }
    }
}
