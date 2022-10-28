using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Business.Abstraction
{
    public interface IAccountRoleService : IBaseService<AccountRole>
    {

        Result Add(AccountRoleDto value);
        Result Update(AccountRoleDto value);
        DataResult<List<Role>> GetRoles();
    }
}
