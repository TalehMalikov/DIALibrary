using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class ExternalSourceService : BaseService, IExternalSourceService
    {
        public async Task<DataResult<List<ExternalSource>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<ExternalSource>>>(BaseUrl + "ExternalSource/getall");
            return result;
        }
    }
}
