using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace FileTester.Services.Abstract
{
    public interface IFileService
    {
        Task<DataResult<List<File>>> GetAll();
        Task<DataResult<List<File>>> GetNewAddedBooks();
        Task<DataResult<File>> Get(int id);
        Task<Result> Add(File publication);
        Task<Result> Delete(int id);
    }
}
