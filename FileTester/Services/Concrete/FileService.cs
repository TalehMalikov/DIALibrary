using FileTester.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace FileTester.Services.Concrete
{
    public class FileService : BaseService,IFileService
    {
        public async Task<DataResult<File>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + "File/get/" + id);
            return result;
        }

        public async Task<Result> Add(File file)
        {
            using HttpClient client = new HttpClient();

            var result = await client.GetJsonAsync<Result>(BaseUrl + "File/add/" + file);
            return result;
        }

        public async Task<DataResult<List<File>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getall");
            return result;
        }
        public async Task<DataResult<List<File>>> GetNewAddedBooks()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getnewadded");
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<Result>(BaseUrl + "File/delete/" + id);
            return result;
        }
    }
}
