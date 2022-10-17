using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<Category>>> GetAll();
    }
}
