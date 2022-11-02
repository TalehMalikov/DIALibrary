using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IAuthService
    {
        Task<DataResult<LoginResponseDto>> Login(AccountLoginDto account);
        Task<DataResult<Account>> GetAccountByAccountName(string token, string accountName);
    }
}
