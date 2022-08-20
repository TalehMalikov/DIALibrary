using Library.Core.Domain.Dtos;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private const string BaseUrl = "https://localhost:7185/api/Authentication/";

        public async Task<DataResult<LoginResponseDto>> Login(AccountLoginDto value)
        {
            using HttpClient client = new HttpClient();
            var result = await client.PostJsonAsync<DataResult<LoginResponseDto>, AccountLoginDto>(BaseUrl + "login", value);
            return result;
        }
    }
}
