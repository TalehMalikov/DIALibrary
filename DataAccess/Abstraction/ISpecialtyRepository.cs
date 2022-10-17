using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface ISpecialtyRepository : ICrudRepository<Specialty>
    {
        bool Add(SpecialtyDto value);
        bool Update(SpecialtyDto value);
    }
}
