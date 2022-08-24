using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IBookRepository : ICrudRepository<Entities.Concrete.File>
    {
    }
}
