using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IAccountService : IBaseService<Account>
    {

        DataResult<int> Add(AccountDto value);
        Result Update(AccountDto value);
        DataResult<List<Role>> GetRoles(Account account);
        DataResult<Account> GetByEmail(string email);
        DataResult<Account> GetByAccountName(string accountName);
        DataResult<Account> Login(string accountName);
    }
}
