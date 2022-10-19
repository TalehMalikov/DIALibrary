using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Concrete
{
    public class SpecialtyService : BaseService, ISpecialtyService
    {
        public async Task<Result> Add(string token, SpecialtyDto entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, SpecialtyDto>(BaseUrl + "Specialty/add", entity);
            return result;
        }

        public async Task<Result> Update(string token, SpecialtyDto entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, SpecialtyDto>(BaseUrl + "Specialty/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Specialty/delete/"+id);
            return result;
        }

        public async Task<DataResult<List<Specialty>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Specialty>>>(BaseUrl + "Specialty/getall");
            return result;
        }
    }
}
