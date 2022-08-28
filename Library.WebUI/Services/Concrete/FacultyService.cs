using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class FacultyService : BaseService, IFacultyService
    {
        public async Task<DataResult<List<Faculty>>> GetAllFaculties(string accessToken)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.GetJsonAsync<DataResult<List<Faculty>>>(BaseUrl + "Faculty/getall");
            return result;
        }
    }
}
