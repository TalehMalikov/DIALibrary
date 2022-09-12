using Library.Admin.Services.Abstract;
using Library.Core.Domain.Dtos;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class AuthService : BaseService, IAuthService
    {
        public async Task<DataResult<LoginResponseDto>> Login(AccountLoginDto account)
        {
            using HttpClient client = new HttpClient();
            var result =
                await client.PostJsonAsync<DataResult<LoginResponseDto>, AccountLoginDto>(BaseUrl + "Authentication/login",account);
            return result;
        }
    }
}
