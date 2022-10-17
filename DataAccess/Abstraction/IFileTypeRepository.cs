using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IFileTypeRepository : ICrudRepository<FileType>
    {
        bool Add(FileType value);
        bool Update(FileType value);
    }
}
