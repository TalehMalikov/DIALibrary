using Library.Entities.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.DataAccess.Abstraction
{
    public interface IFileAuthorRepository : ICrudRepository<BookAuthor>
    {
        FileAuthorDto GetFileWithAuthors(int fileId);
    }
}
