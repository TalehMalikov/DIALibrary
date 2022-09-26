using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using File = Library.Entities.Concrete.File;
namespace Library.Admin.Services.Concrete
{
    public class FileService : BaseService, IFileService
    {
        public async Task<Result> Add(string token, File file)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, File>(BaseUrl + "File/add", file);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + $"File/delete?id={id}");
            return result;
        }

        public async Task<DataResult<File>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + $"File/getbyid/{id}");
            return result;
        }

        public async Task<DataResult<List<File>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getall");
            return result;
        }

        public async Task<Result> Update(string token, File file)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, File>(BaseUrl + "File/update", file);
            return result;
        }
    }
}
