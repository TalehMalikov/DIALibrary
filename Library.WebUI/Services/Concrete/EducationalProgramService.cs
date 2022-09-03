using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class EducationalProgramService : BaseService, IEducationalProgramService
    {
        public async Task<DataResult<List<EducationalProgram>>> GetAll(string accessToken)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.GetJsonAsync<DataResult<List<EducationalProgram>>>(BaseUrl+"EducationalProgram/getall");
            return result;
        }

        public async Task<DataResult<EducationalProgram>> GetByGUID(string accessToken, string guid)
        {
            var educationalPrograms = await GetAll(accessToken);
            var result = educationalPrograms.Data.Where(f => f.GUID == guid).ToList();
            if (result.Count == 1)
                return new SuccessDataResult<EducationalProgram>(result[0]);
            return null;
        }
    }
}
