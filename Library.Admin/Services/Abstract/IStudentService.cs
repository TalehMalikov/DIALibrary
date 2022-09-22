using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IStudentService : IBaseService<Student>
    {
        Task<DataResult<List<Student>>> GetAll(string token);
    }
}
