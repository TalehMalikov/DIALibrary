using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IUserService : IBaseService<User>
    {
        DataResult<int> AddAsStudent(User student);
    }
}