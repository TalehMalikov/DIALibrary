﻿using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class SpecialtyService : BaseService, ISpecialtyService
    {
        public Task<Result> Add(string token, Specialty entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(string token, Specialty entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string token, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<Specialty>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Specialty>>>(BaseUrl + "Specialty/getall");
            return result;
        }
    }
}