﻿using Library.Core.Result.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IFileAuthorService
    {
        Task<DataResult<FileAuthorDto>> GetFileWithAuthors(int fileId);
        Task<Result> Add(string token, FileAuthorDtoForCrud fileAuthorDto);
        Task<Result> Update(string token, FileAuthorDtoForCrud fileAuthorDto);
        Task<Result> AddAllFileAuthors(string token, FileAuthorDtoForCrud fileAuthorDtoForCrud);
        Task<Result> DeleteFileAuthor(string token, int fileId);
        Task<DataResult<List<int>>> GetAllFileAuthors(int fileId);
        
    }
}
