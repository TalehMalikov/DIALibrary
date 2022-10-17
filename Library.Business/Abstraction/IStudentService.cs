using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IStudentService 
    {
        Result Add(StudentDto value);
        Result Update(StudentDto value);
        Result Delete(int id);
        DataResult<Student> Get(int id);
        DataResult<List<Student>> GetAll();
        DataResult<Student> GetByUserId(int id);
    }
}
