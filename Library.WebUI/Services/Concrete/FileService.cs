﻿using Library.Core.Extensions;
using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
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
            var result = await client.GetJsonAsync<DataResult<List<File>>>(BaseUrl + "File/getall");
            return result;
        }

        public async Task<DataResult<File>> GetFileById(int id)
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<File>>(BaseUrl + "File/getbyid/" + id);
            return result;
        }

        public async Task<DataResult<FileAuthorDto>> GetFileWithAuthors(int fileId)
        {
            using HttpClient client = new HttpClient();
            var result =
                await client.GetJsonAsync<DataResult<FileAuthorDto>>(BaseUrl + "File/getfilewithauthors/"+fileId);
            return result;
        }

        public async Task<DataResult<List<File>>> GetFilesByFileTypeId(int fileTypeId)
        {
            var allFiles = await GetAllFiles();
            var filteredFiles = allFiles.Data.Where(f => f.FileType.Id == fileTypeId).ToList();
            return new DataResult<List<File>>(filteredFiles,true);
        }

        public async Task<DataResult<List<FileAuthorDto>>> GetAllFilesWithAuthors()
        {
            using HttpClient client = new HttpClient();
            var result = await client.GetJsonAsync<DataResult<List<FileAuthorDto>>>(BaseUrl + "File/getallfileswithauthors");
            return result;
        }

        public async Task<DataResult<int>> GetFileIdByGUID(string guid)
        {
            var files = await GetAllFiles();
            var result = files.Data.Where(f => f.GUID == guid).ToList();
            if (result.Count == 1)
                return new SuccessDataResult<int>(result[0].Id);
            return null;
        }
    }
}
