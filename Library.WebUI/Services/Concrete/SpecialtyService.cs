﻿using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.WebUI.Services.Abstract;
using System.Net.Http.Headers;

namespace Library.WebUI.Services.Concrete
{
    public class SpecialtyService : BaseService, ISpecialtyService
    {
        public async Task<DataResult<List<Specialty>>> GetAllSpecialties(string accessToken)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var result = await client.GetJsonAsync<DataResult<List<Specialty>>>(BaseUrl + "Specialty/getall");
            return result;
        }
    }
}
