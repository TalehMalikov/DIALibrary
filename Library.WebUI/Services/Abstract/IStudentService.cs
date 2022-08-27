using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IStudentService
    {
        Task<DataResult<Student>> GetUserById(int id);
        Task<Result> Update(Student student);
    }
}
