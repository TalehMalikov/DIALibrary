using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IActivityService : IBaseService<Activity>
    {

        Result Add(Activity value);
        Result Update(Activity value);
        Result DeleteFromDb(int id);
        DataResult<List<Activity> GetDeletedActivities();
    }
}
