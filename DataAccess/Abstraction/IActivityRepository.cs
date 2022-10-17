using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IActivityRepository : ICrudRepository<Activity>
    {
        bool Add(Activity value);
        bool Update(Activity value);
    }
}
