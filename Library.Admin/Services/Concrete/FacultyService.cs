using System.Net.Http.Headers;
using System.Net.NetworkInformation;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class FacultyService : BaseService, IFacultyService
    {
        public async Task<Result> Add(string token, Faculty entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, Faculty>(BaseUrl + "Faculty/add", entity);
            return result;
        }

        public Task<Result> Update(string token, Faculty entity)
        {
            throw new NotImplementedException();
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Faculty/delete/"+id);
            return result;
        }

        public async Task<DataResult<List<Faculty>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Faculty>>>(BaseUrl + "Faculty/getall");
            return result;
        }
    }
}
