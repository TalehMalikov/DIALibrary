using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<DataResult<Student>> GetUserById(string accessToken, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.GetJsonAsync<DataResult<Student>>(BaseUrl + "Student/getbyuserid/" + id);
            return result;
        }
        public async Task<Result> Update(string accessToken, Student student)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.PutJsonAsync<Result, Student>(BaseUrl + "Student/update", student);
            return result;
        }
    }
}
