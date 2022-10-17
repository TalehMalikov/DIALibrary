using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Business.Abstraction
{
    public interface IAccountRoleService : IBaseService<AccountRole>
    {

        Result Add(AccountRole value);
        Result Update(AccountRole value);
    }
}
