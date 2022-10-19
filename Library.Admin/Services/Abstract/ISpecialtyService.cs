using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface ISpecialtyService
    {
        Task<Result> Add(string token, SpecialtyDto entity);
        Task<Result> Update(string token, SpecialtyDto entity);
        Task<Result> Delete(string token, int id);
        Task<DataResult<List<Specialty>>> GetAll(string token);
    }
}
