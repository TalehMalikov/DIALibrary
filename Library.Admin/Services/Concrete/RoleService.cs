using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class RoleService : BaseService, IRoleService
    {
        public async Task<DataResult<Role>> Get(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<Role>>(BaseUrl + "Role/getbyid/" + id);
            return result;
        }

        public Task<DataResult<List<Role>>> GetAll(string token)
        {
            throw new NotImplementedException();
        }
    }
}
