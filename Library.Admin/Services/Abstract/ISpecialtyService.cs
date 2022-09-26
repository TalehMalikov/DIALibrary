using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface ISpecialtyService : IBaseService<Specialty>
    {
        Task<DataResult<List<Specialty>>> GetAll(string token);
    }
}
