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
            var result = await client.GetJsonAsync<DataResult<Publication>>(BaseUrl + "Publication/get/" + id);
            return result;
        }

        public async Task<DataResult<List<Category>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Category>>>(BaseUrl + "Category/getall");
            return result;
        }
        public async Task<DataResult<List<Publication>>> GetNewAddedBooks()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Publication>>>(BaseUrl + "Publication/getnewadded");
            return result;
        }
    }
}
