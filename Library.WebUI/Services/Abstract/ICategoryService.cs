using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<Category>>> GetAll();
        Task<DataResult<List<Publication>>> GetNewAddedBooks();
        Task<DataResult<Publication>> Get(int id);
    }
}
