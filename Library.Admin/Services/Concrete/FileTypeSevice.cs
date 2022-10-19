using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class FileTypeSevice : BaseService, IFileTypeService
    {
        public async Task<Result> Add(string token, FileType entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, FileType>(BaseUrl + "FileType/add", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + $"FileType/delete?id={id}");
            return result;
        }

        public async Task<DataResult<FileType>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<FileType>>(BaseUrl + $"FileType/getbyid/{id}");
            return result;
        }

        public async Task<DataResult<List<FileType>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<FileType>>>(BaseUrl + "FileType/getall");
            return result;
        }

        public async Task<Result> Update(string token, FileType entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, FileType>(BaseUrl + "FileType/update", entity);
            return result;
        }
    }
}
