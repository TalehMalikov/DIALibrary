using FileTester.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace FileTester.Services.Concrete
{
    public class PublicationService : BaseService,IFileService
    {
        public async Task<DataResult<File>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + "Publication/get/" + id);
            return result;
        }

        public async Task<Result> Add(File publication)
        {
            using HttpClient client = new HttpClient();

            var result = await client.GetJsonAsync<Result>(BaseUrl + "Publicaion/add/" + publication);
            return result;
        }

        public async Task<DataResult<List<File>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "Publication/getall");
            return result;
        }
        public async Task<DataResult<List<File>>> GetNewAddedBooks(int count)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "Publication/getnewadded/" + count);
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<Result>(BaseUrl + "Publication/delete/" + id);
            return result;
        }

        public Task<DataResult<List<File>>> GetNewAddedBooks()
        {
            throw new NotImplementedException();
        }
    }
}
