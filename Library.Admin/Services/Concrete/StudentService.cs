using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<Result> Add(string token, Student student)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, Student>(BaseUrl + "Student/add", student);
            return result;
        }

        public Task<Result> Update(string token, Student entity)
        {
            throw new NotImplementedException();
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
    }
}
