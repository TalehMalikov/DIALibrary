using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class EducationalProgramService : BaseService, IEducationalProgramService
    {
        public async Task<Result> Add(string token, EducationalProgramDto educationalProgramDto)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PostJsonAsync<Result, EducationalProgramDto>(BaseUrl + "EducationalProgram/add", educationalProgramDto);
            return result;
        }

        public async Task<Result> Delete(string token, int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.DeleteJsonAsync<Result>(BaseUrl + $"EducationalProgram/delete/{id}");
            return result;
        }

        public async Task<DataResult<EducationalProgram>> Get(string token,int id)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<EducationalProgram>>(BaseUrl + $"EducationalProgram/getbyid/{id}");
            return result;
        }

        public async Task<DataResult<List<EducationalProgram>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<EducationalProgram>>>(BaseUrl + "EducationalProgram/getall");
            return result;
        }

        public async Task<Result> Update(string token, EducationalProgramDto educationalProgramDto)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.PutJsonAsync<Result, EducationalProgramDto>(BaseUrl + "EducationalProgram/update", educationalProgramDto);
            return result;
        }
    }
}
