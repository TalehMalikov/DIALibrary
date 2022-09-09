using Library.Core.Domain.Dtos;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IAccountService _accountService;
        public AuthService(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public async Task<DataResult<LoginResponseDto>> Login(AccountLoginDto value)
        {
            using HttpClient client = new HttpClient();
            var result = await client.PostJsonAsync<DataResult<LoginResponseDto>, AccountLoginDto>(BaseUrl + "Authentication/login", value);
            return result;
        }

        public async Task<Result> ResetPassword()
        {
            //using HttpClient client = new HttpClient();
            //var result = client.PutJsonAsync().
            return null;
        }
    }
}
