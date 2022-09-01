using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class EducationalProgramService : BaseService, IEducationalProgramService
    {
        public async Task<DataResult<List<EducationalProgram>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<EducationalProgram>>>(BaseUrl+"EducationalProgram/getall");
            return result;
        }

        public Task<DataResult<EducationalProgram>> GetById(int id, string token)
        {
            throw new NotImplementedException();
        }
    }
}
