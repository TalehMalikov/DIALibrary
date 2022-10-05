using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class SectorService : BaseService, ISectorService
    {
        public Task<Result> Add(string token, Sector entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(string token, Sector entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string token, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<Sector>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Sector>>>(BaseUrl + "Sector/getall");
            return result;
        }
    }
}
