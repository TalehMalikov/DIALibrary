using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface IStudentRepository : ICrudRepository<Student>
    {
        bool Add(StudentDto value);
        bool Update(StudentDto value);
        Student GetByUserId(int id);
    }
}
