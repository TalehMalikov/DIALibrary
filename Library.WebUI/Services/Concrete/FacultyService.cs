using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class FacultyService : BaseService, IFacultyService
    {
        public async Task<DataResult<List<Faculty>>> GetAllFaculties()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Faculty>>>(BaseUrl + "Faculty/getall");
            return result;
        }
    }
}
