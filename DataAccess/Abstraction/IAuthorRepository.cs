using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IAuthorRepository : ICrudRepository<Author>
    {
        bool Add(Author value);
        bool Update(Author value);
    }
}
