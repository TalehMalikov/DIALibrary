using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.DataAccess.Abstraction
{
    public interface IAccountRoleRepository : ICrudRepository<AccountRole>
    {
        bool Add(AccountRoleDto value);
        bool Update(AccountRoleDto value);
        List<Role> GetRoles();
    }
}
