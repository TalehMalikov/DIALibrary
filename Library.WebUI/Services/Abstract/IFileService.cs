using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Abstract
{
    public interface IFileService
    {
        Task<DataResult<List<File>>> GetAllFilesByCategoryId(int id);
        Task<DataResult<List<File>>> GetFilesByFileTypeId(int fileTypeId);
        Task<DataResult<List<File>>> GetAllFiles();
        Task<DataResult<File>> GetFileById(int id);
        Task<DataResult<FileAuthorDto>> GetFileWithAuthors(int fileId);
        Task<DataResult<List<FileAuthorDto>>> GetAllFilesWithAuthors();
    }
}
