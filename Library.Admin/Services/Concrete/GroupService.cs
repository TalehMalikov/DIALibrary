using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Concrete
{
    public class GroupService : BaseService , IGroupService
    {
        public async Task<Result> Add(string token, GroupDto entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, GroupDto>(BaseUrl + "Group/add", entity);
            return result;
        }

        public async Task<Result> Update(string token, GroupDto entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, GroupDto>(BaseUrl + "Group/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Group/delete/"+ id);
            return result;
        }

        public async Task<DataResult<List<Group>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Group>>>(BaseUrl + "Group/getall");
            return result;
        }

        public async Task<DataResult<Group>> Get(string accessToken, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.GetJsonAsync<DataResult<Group>>(BaseUrl + "Group/getbyid/"+id);
            return result;
        }
    }
}
