using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IUserService
    {
        Task<Result> Add(User user, string token);
        Task<Result> Delete(int id, string token);
        Task<DataResult<List<User>>> GetAll(string token);
    }
}
