using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<Category>>> GetAll();
        Task<DataResult<Category>> Get(int id);
        Task<Result> Add(string token, Category category);
        Task<Result> Delete(string token,int id);
        Task<Result> Update(string token,Category category);
    }
}
