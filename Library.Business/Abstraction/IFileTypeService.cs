using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IFileTypeService : IBaseService<FileType>
    {
        Result Add(FileType value);
        Result Update(FileType value);
    }
}
