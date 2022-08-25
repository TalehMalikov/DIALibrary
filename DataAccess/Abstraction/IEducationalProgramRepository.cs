using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IEducationalProgramRepository : ICrudRepository<EducationalProgram>
    {
        List<EducationalProgram> GetAllBySpecialtyId(int id);
    }
}
