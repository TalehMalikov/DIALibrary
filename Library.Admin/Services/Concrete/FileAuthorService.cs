using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class FileAuthorService : BaseService, IFileAuthorService
    {
        public async Task<Result> Add(string token, FileAuthorDtoForCrud fileAuthorDto)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, FileAuthorDtoForCrud>(BaseUrl + "BookAuthor/add", fileAuthorDto);
            return result;
        }

        public async Task<DataResult<FileAuthorDto>> GetFileWithAuthors(int fileId)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<FileAuthorDto>>(BaseUrl + "File/getfilewithauthors/" + fileId);
            return result;
        }

        public async Task<Result> Update(string token, FileAuthorDtoForCrud fileAuthorDto)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, FileAuthorDtoForCrud>(BaseUrl + "BookAuthor/update", fileAuthorDto);
            return result;
        }

        public async Task<Result> AddAllFileAuthors(string token,FileAuthorDtoForCrud fileAuthorDtoForCrud)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result,FileAuthorDtoForCrud>(BaseUrl + "BookAuthor/addallfileauthors/", fileAuthorDtoForCrud);
            return result;
        }
        public async Task<DataResult<List<int>>> GetAllFileAuthors(int fileId)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<int>>>(BaseUrl + "BookAuthor/getallfileauthors/" + fileId);
            return result;
        }

        public async Task<Result> DeleteFileAuthor(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "BookAuthor/deletefileauthor/" + id);
            return result;
        }
    }
}
