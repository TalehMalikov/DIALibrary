using Library.Business.Abstraction;
using Library.Business.CrossCuttingConcerns.Validation.FluentValidation;
using Library.Core.Aspects.Autofac.Caching;
using Library.Core.Aspects.Autofac.Validation;
using Library.Core.Result.Concrete;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryManager(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ICategoryService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Add(Category value)
        {
            _categoryRepository.Add(value);
            return new SuccessResult();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ICategoryService.Get))]
        public Result Delete(int id)
        {
            _categoryRepository.Delete(id);
            return new SuccessResult();
        }

        [CacheAspect]
        public DataResult<Category> Get(int id)
        {
            return new SuccessDataResult<Category>(_categoryRepository.Get(id));
        }

        [CacheAspect]
        public DataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categoryRepository.GetAll());
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ICategoryService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Update(Category value)
        {
            _categoryRepository.Update(value);
            return new SuccessResult();
        }
    }
}
