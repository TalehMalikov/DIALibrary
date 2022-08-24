using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class CategoryService : BaseService,ICategoryService
    {
        public async Task<DataResult<Publication>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Publication>>(_baseUrl + "Publication/get/" + id);
            return result;
        }

        public async Task<DataResult<List<Category>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Category>>>(_baseUrl + "Category/getall");
            return result;
        }
        public async Task<DataResult<List<Publication>>> GetNewAddedBooks(int count)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Publication>>>(_baseUrl + "Publication/getnewadded/"+count);
            return result;
        }
    }
}
