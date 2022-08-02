using Library.Core.Result.Abstract;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IAccountService
    {
        DataResult<List<Role>> GetRoles(User user);
        IResult Add(User user);
        IDataResult<User> GetByEmail(string email);
    }
}
