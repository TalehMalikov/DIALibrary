using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IActivityService
    {
        Task<Result> Add(string token, Activity activity);
    }
}
