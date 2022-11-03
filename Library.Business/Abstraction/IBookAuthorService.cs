using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IBookAuthorService : IBaseService<BookAuthor>
    {
        Result Add(FileAuthorDtoForCrud value);
        Result AddList(List<FileAuthorDtoForCrud> value);
        Result Update(FileAuthorDtoForCrud value);
        Result UpdateList(List<FileAuthorDtoForCrud> value);
        Result AddAllFilesAuthor(List<int> authorIds,int fileId);
        Result DeleteFileAuthor(int fileId);
        DataResult<List<int>> GetAllFileAuthors(int fileId);
    }
}
