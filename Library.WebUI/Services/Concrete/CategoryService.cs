using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private const string BaseUrl = "https://localhost:7185/api/Category/";

        public async Task<DataResult<List<Category>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Category>>>(BaseUrl + "getall");
            return result;
        }
    }
}
