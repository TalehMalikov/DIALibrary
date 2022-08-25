using Library.Core.Result.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IFileService 
    {
        Task<DataResult<List<File>>>
    }
}
