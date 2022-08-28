using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface ISectorService
    {
        Task<DataResult<List<Sector>>> GetAllSectors(string accessToken);
    }
}
