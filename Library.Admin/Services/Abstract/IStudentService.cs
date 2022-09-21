using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IStudentService
    {
        Task<Result> Add(string token,Student student);
        Task<DataResult<List<Student>>> GetAll(string token);
    }
}
