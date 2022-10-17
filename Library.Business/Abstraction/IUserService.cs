using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IUserService : IBaseService<User>
    {
        Result Add(User value);
        Result Update(User value);
        DataResult<int> AddAsStudent(User student);
        Result DeleteFromDb(int id);
    }
}