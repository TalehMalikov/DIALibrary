using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.Admin.Services.Abstract
{
    public interface IFileService
    {
        Task<Result> Add(string token, FileDto fileDto);
        Task<Result> Update(string token, FileDto fileDto);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<File>>> GetAll();
        Task<DataResult<File>> Get(int id);
    }
}
