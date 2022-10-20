using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IFacultyService 
    {
        Task<Result> Add(string token, Faculty entity);
        Task<Result> Update(string token, Faculty entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Faculty>>> GetAll(string token);
        Task<DataResult<Faculty>> Get(string token, int id);
    }
}
