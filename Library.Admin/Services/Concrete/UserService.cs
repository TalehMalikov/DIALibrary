using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class UserService : BaseService, IUserService
    {
        public async Task<Result> Add(string token,User user)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result,User>(BaseUrl + "User/add", user);
            return result;
        }

        public async Task<Result> Delete(string token,int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "User/delete/"+ id);
            return result;
        }

        public async Task<DataResult<List<User>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<User>>>(BaseUrl + "User/getall");
            return result;
        }

        public async Task<DataResult<int>> AddAsStudent(User user, string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<DataResult<int>, User>(BaseUrl + "User/addasstudent", user);
            return result;
        }

        public async Task<Result> Update(string token, User entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, User>(BaseUrl + "User/update", entity);
            return result;  
        }

    }
}
