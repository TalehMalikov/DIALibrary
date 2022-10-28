using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Concrete
{
    public class AccountRoleService : BaseService , IAccountRoleService
    {
        public async Task<DataResult<List<Role>>> GetRoles(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Role>>>(BaseUrl + "AccountRole/getroles");
            return result;
        }

        public async Task<DataResult<List<AccountRole>>> GetAccountRoles(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<AccountRole>>>(BaseUrl + "AccountRole/getall");
            return result;
        }

        public async Task<DataResult<AccountRole>> GetAccountRole(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<AccountRole>>(BaseUrl + "AccountRole/getbyid/"+id);
            return result;
        }

        public async Task<Result> Add(string token, AccountRoleDto role)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, AccountRoleDto>(BaseUrl + "AccountRole/add", role);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "AccountRole/delete/" + id);
            return result;
        }

        public async Task<Result> Update(string token, AccountRoleDto role)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, AccountRoleDto>(BaseUrl + "AccountRole/update", role);
            return result;
        }
    }
}
