using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IFacultyService : IBaseService<Faculty>
    {
        Task<DataResult<List<Faculty>>> GetAll(string token);
    }
}
