using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IStudentService : IBaseService<Student>
    {
        DataResult<Student> GetByUserId(int id);
    }
}
