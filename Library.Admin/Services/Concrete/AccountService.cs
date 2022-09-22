using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class AccountService : BaseService ,IAccountService
    {
        public Task<Result> Add(string token, Account account)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = client.PostJsonAsync<Result, Account>(BaseUrl + "Account/add", account);
            return result;
        }

        public Task<Result> Update(string token, Account entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string token, int id)
        {
            throw new NotImplementedException();
        }

        public Task<DataResult<List<Account>>> GetAll(string token)
        {
            throw new NotImplementedException();
        }
    }
}
