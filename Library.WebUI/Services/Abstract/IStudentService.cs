using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IStudentService
    {
        Task<DataResult<Student>> GetUserById(string accessToken, int id);
        Task<Result> Update(string accessToken, Student student);
    }
}
