using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
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

        public async Task<Result> Update(Account account)
        {
            using HttpClient client = new HttpClient();
            var result = await client.PutJsonAsync<Result,Account>(BaseUrl + "Account/update", account);
            return result;
        }
    }
}
