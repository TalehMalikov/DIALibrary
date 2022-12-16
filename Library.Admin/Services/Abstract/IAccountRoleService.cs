using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IAccountRoleService
    {
        Task<DataResult<List<Role>>> GetRoles(string token);
        Task<DataResult<List<AccountRole>>> GetAccountRoles(string token);
        Task<DataResult<AccountRole>> GetAccountRole(string token, int id);
        Task<DataResult<AccountRole>> GetAccountRoleByAccountName(string token,string accountName);
        Task<DataResult<AccountRole>> GetAccountRoleByAccountId(string token, int id);
        Task<Result> Add(string token, AccountRoleDto role);
        Task<Result> Delete(string token,int id);
        Task<Result> Update(string token, AccountRoleDto role);
    }
}
