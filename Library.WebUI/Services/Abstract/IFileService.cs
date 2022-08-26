using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Abstract
{
    public interface IFileService 
    {
        Task<DataResult<List<File>>> GetAll(string token);
    }
}
