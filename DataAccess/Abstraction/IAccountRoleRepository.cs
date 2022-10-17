using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IAccountRoleRepository : ICrudRepository<AccountRole>
    {
        bool Add(AccountRole value);
        bool Update(AccountRole value);
    }
}
