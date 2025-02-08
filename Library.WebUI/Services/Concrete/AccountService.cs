using Library.Core.Domain.Dtos;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Library.WebUI.Services.Concrete
{
    public class AccountService : BaseService, IAccountService
    {
        public async Task<DataResult<Account>> GetByEmail(string name)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Account>>(BaseUrl + "Account/getbyemail?name=" + name);
            return result;
        }

        public async Task<DataResult<Account>> GetByAccountName(string accessToken, string accountName)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var account = new ResetPasswordDto()
            {
                AccountName = accountName
            };
            var result = await client.PostJsonAsync<DataResult<Account>, ResetPasswordDto>(BaseUrl + "Account/getbyaccountname", account);
            return result;
        }

        public async Task<Result> Update(string accessToken, Account account)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.PutJsonAsync<Result,Account>(BaseUrl + "Account/update", account);
            return result;
        }

        public async Task<Result> ResetPassword(Account account)
        {
            using HttpClient client = new HttpClient();
            var result = await client.PutJsonAsync<Result, Account>(BaseUrl + "Account/resetpassword", account);
            return result;
        }
    }
}
