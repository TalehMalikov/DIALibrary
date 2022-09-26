using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using System.Net.Http.Headers;

namespace Library.Admin.Services.Concrete
{
    public class LanguageService :BaseService, ILanguageService
    {
        public async Task<DataResult<List<Language>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Language>>>(BaseUrl + "Language/getall");
            return result;
        }
    }
}
