using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface IEducationalProgramRepository : ICrudRepository<EducationalProgram>
    {
        bool Add(EducationalProgramDto value);
        bool Update(EducationalProgramDto value);
        List<EducationalProgram> GetAllBySpecialtyId(int id);
    }
}
