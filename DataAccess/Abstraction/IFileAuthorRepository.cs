using Library.Entities.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.DataAccess.Abstraction
{
    public interface IFileAuthorRepository : ICrudRepository<BookAuthor>
    {
        FileAuthorDto GetFileWithAuthors(int fileId);
        List<FileAuthorDto> GetAllFilesWithAuthors(List<File> files);
        bool AddAllFilesAuthor(List<int> authorIds, int fileId);
        List<int> GetAllFileAuthors(int fileId);
        bool DeleteFileAuthor(int fileId);
    }
}
