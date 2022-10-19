using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IUserService
    {
        Task<Result> Add(string token, User entity);
        Task<Result> Update(string token, User entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<User>>> GetAll(string token);
        Task<DataResult<int>> AddAsStudent(User user, string token);
        Task<Result> DeleteFromDb(int id, string token);
        Task<DataResult<User>> Get(string token,int id);
        Task<DataResult<List<User>>> GetDeactiveUsers(string token);
    }
}
