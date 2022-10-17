using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IFacultyRepository : ICrudRepository<Faculty>
    {
        bool Add(Faculty value);
        bool Update(Faculty value);
    }
}
