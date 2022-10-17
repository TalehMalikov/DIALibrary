using Library.Entities.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.DataAccess.Abstraction
{
    public interface IFileRepository : ICrudRepository<File>
    {
        bool Add(FileDto value);
        bool Update(FileDto value);
        List<File> GetNewAdded();
        List<File> GetAllFilesByCategoryId(int id);
    }
}
