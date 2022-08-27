using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class StudentService : BaseService, IStudentService
    {
        public async Task<DataResult<Student>> GetUserById(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Student>>(BaseUrl + "Student/getbyuserid/" + id);
            return result;
        }
        public async Task<Result> Update(Student student)
        {
            using HttpClient client = new HttpClient();
            var result = await client.PutJsonAsync<Result, Student>(BaseUrl + "Student/update", student);
            return result;
        }
    }
}
