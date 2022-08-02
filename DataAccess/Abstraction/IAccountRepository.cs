using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IAccountRepository : ICrudRepository<Account>
    {
        Account GetByEmail(string email);
        List<Role> GetRoles(Account account);
    }
}
