using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class UserService : BaseService, IUserService
    {
        public async Task<Result> Update(string accessToken, User user)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.PutJsonAsync<Result, User>(BaseUrl + "User/update", user);
            return result;
        }
    }
}
