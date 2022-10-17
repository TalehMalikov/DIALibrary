using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface ISectorRepository : ICrudRepository<Sector>
    {
        bool Add(Sector value);
        bool Update(Sector value);
    }
}
