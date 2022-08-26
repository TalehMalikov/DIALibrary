using File = Library.Entities.Concrete.File;

namespace Library.DataAccess.Abstraction
{
    public interface IFileRepository : ICrudRepository<File>
    {
        List<File> GetNewAdded();
        List<File> GetAllFilesByCategoryId(int id);
    }
}
