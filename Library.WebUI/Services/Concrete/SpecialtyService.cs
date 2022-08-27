using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;

namespace Library.WebUI.Services.Concrete
{
    public class SpecialtyService : BaseService, ISpecialtyService
    {
        public async Task<DataResult<List<Specialty>>> GetAllSpecialties()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<Specialty>>>(BaseUrl + "Specialty/getall");
            return result;
        }
    }
}
