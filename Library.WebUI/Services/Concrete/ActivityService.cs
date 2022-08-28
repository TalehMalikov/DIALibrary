using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class ActivityService : BaseService,IActivityService
    {
        public async Task<DataResult<List<Activity>>> GetAll()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Activity>>>(BaseUrl + "Activity/getall");
            return result;
        }

        public async Task<DataResult<Activity>> Get(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<Activity>>(BaseUrl + "Activity/getbyid/"+id);
            return result;
        }
    }
}
