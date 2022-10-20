using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class AuthorService : BaseService, IAuthorService
    {
        public async Task<Result> Add(string token, Author entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, Author>(BaseUrl + "Author/add", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Author/delete/" + id);
            return result;
        }

        public async Task<DataResult<Author>> Get(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<Author>>(BaseUrl + "Author/getbyid/" + id);
            return result;
        }

        public async Task<DataResult<List<Author>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Author>>>(BaseUrl + "Author/getall");
            return result;
        }

        public async Task<Result> Update(string token, Author entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, Author>(BaseUrl + "Author/update", entity);
            return result;
        }
    }
}
