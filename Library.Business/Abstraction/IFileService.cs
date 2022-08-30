using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.Business.Abstraction
{
    public interface IFileService : IBaseService<File>
    {
        DataResult<List<File>> GetNewAdded();
        DataResult<List<File>> GetAllFilesByCategoryId(int id);
        DataResult<FileAuthorDto> GetFileWithAuthors(int fileId);

    }
}
