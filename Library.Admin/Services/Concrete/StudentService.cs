using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Concrete
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<Result> Add(string token, StudentDto student)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, StudentDto>(BaseUrl + "Student/add", student);
            return result;
        }

        public async Task<Result> Update(string token, StudentDto student)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, StudentDto>(BaseUrl + "Student/update", student);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Student/delete/" + id);
            return result;
        }

        public async Task<DataResult<List<Student>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Student>>>(BaseUrl + "Student/getall");
            return result;
        }

        public async Task<DataResult<Student>> GetByUserId(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<Student>>(BaseUrl + "Student/getbyuserid/" + id);
            return result;
        }
    }
}
