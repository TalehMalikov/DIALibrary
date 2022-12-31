using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Concrete
{
    public class AccountService : BaseService ,IAccountService
    {
        public async Task<DataResult<int>> Add(string token, AccountDto account)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<DataResult<int>, AccountDto>(BaseUrl + "Account/add", account);
            return result;
        }

        public async Task<Result> Update(string token, AccountDto entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, AccountDto>(BaseUrl + "Account/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Account/delete/" + id);
            return result;
        }

        public async Task<DataResult<List<Account>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Account>>>(BaseUrl + "Account/getall");
            return result;
        }

        public async Task<DataResult<Account>> Get(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<Account>>(BaseUrl + "Account/getbyid/" + id);
            return result;
        }
        public async Task<DataResult<Account>> GetByEmail(string name)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Account>>(BaseUrl + "Account/getbyemail?name=" + name);
            return result;
        }
        public async Task<DataResult<Account>> GetByAccountName(string name)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Account>>(BaseUrl + "Account/getbyaccountname?name=" + name);
            return result;
        }
    }
}
