using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<Category>>> GetAll();
        Task<DataResult<Category>> Get(int id);
    }
}
