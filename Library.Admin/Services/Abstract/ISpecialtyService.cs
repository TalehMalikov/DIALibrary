using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ISpecialtyService
    {
        Task<Result> Add(string token, Specialty entity);
        Task<Result> Update(string token, Specialty entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Specialty>>> GetAll(string token);
    }
}
