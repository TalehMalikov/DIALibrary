using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.Business.Abstraction
{
    public interface IFileService : IBaseService<File>
    {
        DataResult<List<File>> GetNewAdded();

    }
}
