using Library.Core.Result.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IBaseService<T> where T : class, new()
    {
        Task<Result> Add(string token, T entity);
        Task<Result> Update(string token, T entity);
        Task<Result> Delete(string token,int id);
    }
}
