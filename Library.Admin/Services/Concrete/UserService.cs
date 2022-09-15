using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using NuGet.Common;

namespace Library.Admin.Services.Concrete
{
    public class UserService : BaseService, IUserService
    {
        public async Task<Result> Add(User user, string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result,User>(BaseUrl + "User/add", user);
            return result;
        }

        public async Task<Result> Delete(int id, string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, int>(BaseUrl + "User/delete/", id);
            return result;
        }

        public async Task<DataResult<List<User>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<User>>>(BaseUrl + "User/getall");
            return result;
        }
    }
}
