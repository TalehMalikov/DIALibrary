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
            return new SuccessResult(StatusMessagesUtil.AddSuccessMessage);
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ICategoryService.Get))]
        public Result Delete(int id)
        {
            if(_categoryRepository.Delete(id))
                return new SuccessResult(StatusMessagesUtil.DeleteSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }

        [CacheAspect]
        public DataResult<Category> Get(int id)
        {
            var result = _categoryRepository.Get(id);
            if (result == null)
                return new ErrorDataResult<Category>(result, StatusMessagesUtil.NotFoundMessageGivenId);
            return new SuccessDataResult<Category>(result);
        }

        [CacheAspect]
        public DataResult<List<Category>> GetAll()
        {
            var result = _categoryRepository.GetAll();
            if (result.Count == 0)
                return new ErrorDataResult<List<Category>>(result,StatusMessagesUtil.NotFoundMessage);
            return new SuccessDataResult<List<Category>>();
        }

        [CacheRemoveAspect(nameof(Library.Business.Abstraction.ICategoryService.Get))]
        [ValidationAspect(typeof(CategoryValidator))]
        public Result Update(Category value)
        {
            if(_categoryRepository.Update(value))
                return new SuccessResult(StatusMessagesUtil.UpdateSuccessMessage);
            return new ErrorResult(StatusMessagesUtil.NotFoundMessageGivenId);
        }
    }
}
