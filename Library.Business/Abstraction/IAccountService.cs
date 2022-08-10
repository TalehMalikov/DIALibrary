using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IAccountService : IBaseService<Account>
    {
        DataResult<List<Role>> GetRoles(Account account);
        //IResult Add(User user);
        DataResult<Account> GetByEmail(string email);
    }
}
