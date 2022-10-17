using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IFileTypeService : IBaseService<FileType>
    {
        Task<DataResult<List<FileType>>> GetAll();
        Task<DataResult<FileType>> Get(int id);
    }
}
