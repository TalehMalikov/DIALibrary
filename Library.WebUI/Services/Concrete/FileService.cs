using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Concrete
{
    public class FileService : BaseService, IFileService
    {
        public async Task<DataResult<List<File>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getall");
            return result;
        }
    }
}
