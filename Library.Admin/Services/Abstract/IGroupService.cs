using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IGroupService 
    {
        Task<Result> Add(string token, GroupDto entity);
        Task<Result> Update(string token, GroupDto entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Group>>> GetAll(string token);
        Task<DataResult<Group>> Get(string accessToken, int id);
    }
}
