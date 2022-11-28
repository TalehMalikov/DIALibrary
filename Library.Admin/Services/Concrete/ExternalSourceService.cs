using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class ExternalSourceService : BaseService, IExternalSourceService
    {
        public async Task<Result> Add(string token, ExternalSource entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, ExternalSource>(BaseUrl + "ExternalSource/add", entity);
            return result;
        }

        public async Task<Result> Update(string token, ExternalSource entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, ExternalSource>(BaseUrl + "ExternalSource/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "ExternalSource/delete/" + id);
            return result;
        }

        public async Task<DataResult<List<ExternalSource>>> GetAll()
        {
            using var client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<ExternalSource>>>(BaseUrl + "ExternalSource/getall");
            return result;
        }

        public async Task<DataResult<ExternalSource>> Get(string token, int id)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<ExternalSource>>(BaseUrl + "ExternalSource/getbyid/"+id);
            return result;
        }
    }
}
