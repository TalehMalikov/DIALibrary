﻿using System.Net.Http.Headers;
using Library.Admin.Services.Abstract;
using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Concrete
{
    public class GroupService : BaseService , IGroupService
    {
        public Task<Result> Add(string token, Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Update(string token, Group entity)
        {
            throw new NotImplementedException();
        }

        public Task<Result> Delete(string token, int id)
        {
            throw new NotImplementedException();
        }

        public async Task<DataResult<List<Group>>> GetAll(string token)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var result = await client.GetJsonAsync<DataResult<List<Group>>>(BaseUrl + "Group/getall");
            return result;
        }
    }
}