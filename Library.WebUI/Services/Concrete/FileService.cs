﻿using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.WebUI.Services.Abstract;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Services.Concrete
{
    public class FileService : BaseService, IFileService
    {
        public async Task<DataResult<List<File>>> GetAllFilesByCategoryId(int id)
        {
            using HttpClient client = new HttpClient();
            var result =
                await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getallfilesbycategoryid/" + id);
            return result;
        }

        public async Task<DataResult<List<File>>> GetAllFiles()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "Files/getall");
            return result;
        }

        public async Task<DataResult<File>> GetFileById(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + "get/" + id);
            return result;
        }
    }
}