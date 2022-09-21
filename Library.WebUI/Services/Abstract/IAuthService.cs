using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;

namespace Library.WebUI.Services.Abstract
{
    public interface IAuthService
    {
        Task<DataResult<LoginResponseDto>> Login(AccountLoginDto value);
    }
}
