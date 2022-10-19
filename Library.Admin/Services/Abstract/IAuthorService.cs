using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IAuthorService
    {
        Task<Result> Add(string token, Author entity);
        Task<Result> Update(string token, Author entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Author>>> GetAll(string token);
    }
}
