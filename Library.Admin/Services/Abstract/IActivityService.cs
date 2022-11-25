using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IActivityService
    {
        Task<Result> Add(string token, Activity entity);
        Task<Result> Activate(string token,int id);
        Task<Result> Update(string token, Activity entity);
        Task<Result> Delete(string token, int id);
        Task<Result> DeleteFromDb(string token, int id);
        Task<DataResult<List<Activity>>> GetAll();
        Task<DataResult<List<Activity>>> GetDeletedActivities(string token);
        
        Task<DataResult<Activity>> Get(int id);

    }
}
