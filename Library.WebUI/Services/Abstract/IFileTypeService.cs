using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IFileTypeService
    {
        Task<DataResult<List<FileType>>> GetAllFileTypes();
    }
}
