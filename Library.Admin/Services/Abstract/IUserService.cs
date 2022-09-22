using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IUserService : IBaseService<User>
    {
        Task<DataResult<List<User>>> GetAll(string token);
        Task<DataResult<int>> AddAsStudent(User user, string token);
    }
}
