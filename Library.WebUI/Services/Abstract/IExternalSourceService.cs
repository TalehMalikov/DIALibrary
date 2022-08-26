using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IExternalSourceService
    {
        Task<DataResult<List<ExternalSource>>> GetAll();
    }
}
