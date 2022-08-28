using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IActivityService
    {
        Task<DataResult<List<Activity>>> GetAll();
        Task<DataResult<Activity>> Get(int id);
    }
}
