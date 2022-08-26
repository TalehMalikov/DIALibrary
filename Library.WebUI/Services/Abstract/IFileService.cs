using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Abstract
{
    public interface IFileService
    {
        Task<DataResult<List<File>>> GetAllFilesByCategoryId(int id);
        Task<DataResult<List<File>>> GetAllFiles();
        Task<DataResult<File>> GetFileById(int id);
        Task<Result> Delete(int id);
    }
}
