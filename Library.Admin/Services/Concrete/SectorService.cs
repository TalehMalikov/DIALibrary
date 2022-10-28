using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class SectorService : BaseService, ISectorService
    {
        public async Task<Result> Add(string token, Sector entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, Sector>(BaseUrl + "Sector/add", entity);
            return result;
        }

        public async Task<Result> Update(string token, Sector entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, Sector>(BaseUrl + "Sector/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Sector/delete" + id);
            return result;
        }

        public async Task<DataResult<List<Sector>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Sector>>>(BaseUrl + "Sector/getall");
            return result;
        }

        public async Task<DataResult<Sector>> Get(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<Sector>>(BaseUrl + "Sector/getbyid/" + id);
            return result;
        }
    }
}
