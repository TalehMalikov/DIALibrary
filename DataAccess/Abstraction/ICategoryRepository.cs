using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface ICategoryRepository : ICrudRepository<Category>
    {
        bool Add(Category value);
        bool Update(Category value);
    }
}
