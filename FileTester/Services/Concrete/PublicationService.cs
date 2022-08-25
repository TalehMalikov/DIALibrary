using FileTester.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;

namespace FileTester.Services.Concrete
{
    public class PublicationService : BaseService,IPublicationService
    {
        public async Task<DataResult<Publication>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Publication>>(BaseUrl + "Publication/get/" + id);
            return result;
        }

        public async Task<Result> Add(Publication publication)
        {
            using HttpClient client = new HttpClient();

            var result = await client.GetJsonAsync<Result>(BaseUrl + "Publicaion/add/" + publication);
            return result;
        }

        public async Task<DataResult<List<Publication>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Publication>>>(BaseUrl + "Publication/getall");
            return result;
        }
        public async Task<DataResult<List<Publication>>> GetNewAddedBooks(int count)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Publication>>>(BaseUrl + "Publication/getnewadded/" + count);
            return result;
        }

        public async Task<Result> Delete(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<Result>(BaseUrl + "Publication/delete/" + id);
            return result;
        }
    }
}
