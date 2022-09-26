using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.Admin.Services.Abstract
{
    public interface IFileService
    {
        Task<Result> Add(string token, File file);
        Task<Result> Update(string token, File file);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<File>>> GetAll();
        Task<DataResult<File>> Get(int id);
    }
}
