using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class CategoryService : BaseService, ICategoryService
    {
        public async Task<DataResult<List<Category>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Category>>>(BaseUrl + "Category/getall");
            return result;
        }
    }
}
