using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface IAccountRepository : ICrudRepository<Account>
    {
        bool Add(AccountDto value);
        bool Update(AccountDto value);
        Account GetByEmail(string email);
        List<Role> GetRoles(Account account);
    }
}
