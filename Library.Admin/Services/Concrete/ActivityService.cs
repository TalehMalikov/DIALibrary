using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using System.Net.Http.Headers;
using Activity = Library.Entities.Concrete.Activity;

namespace Library.Admin.Services.Concrete
{
    public class ActivityService : BaseService, IActivityService
    {
        public async Task<Result> Add(string token, Activity activity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, Activity>(BaseUrl + "Activity/add", activity);
            return result;
        }

        public async Task<Result> Activate(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result,int>(BaseUrl + "Activity/activate",id);
            return result;
        }

        public async Task<Result> Update(string token, Activity entity)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, Activity>(BaseUrl + "Activity/update", entity);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Activity/delete/" + id);
            return result;
        }

        public async Task<Result> DeleteFromDb(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + "Activity/deletefromdb/" + id);
            return result;
        }

        public async Task<DataResult<List<Activity>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Activity>>>(BaseUrl + "Activity/getall");
            return result;
        }

        public async Task<DataResult<List<Activity>>> GetDeletedActivities(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Activity>>>(BaseUrl + "Activity/getdeletedactivities");
            return result;
        }

        public async Task<DataResult<Activity>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Activity>>(BaseUrl + "Activity/getbyid/" + id);
            return result;
        }

        
    }
}
