using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Concrete
{
    public class CategoryService : BaseService,ICategoryService
    {
        public async Task<DataResult<File>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + "File/get/" + id);
            return result;
        }

        public async Task<DataResult<List<Category>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Category>>>(BaseUrl + "Category/getall");
            return result;
        }
        public async Task<DataResult<List<File>>> GetNewAddedBooks()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getnewadded");
            return result;
        }
    }
}
