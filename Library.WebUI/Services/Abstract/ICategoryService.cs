using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Abstract
{
    public interface ICategoryService
    {
        Task<DataResult<List<Category>>> GetAll();
        Task<DataResult<List<File>>> GetNewAddedBooks();
        Task<DataResult<File>> Get(int id);
    }
}
