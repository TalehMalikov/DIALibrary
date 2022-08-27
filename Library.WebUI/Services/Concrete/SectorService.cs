using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class SectorService : BaseService, ISectorService
    {
        public async Task<DataResult<List<Sector>>> GetAllSectors()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Sector>>>(BaseUrl + "Sector/getall");
            return result;
        }
    }
}
