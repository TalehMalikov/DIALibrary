using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IRoleService
    {
        Task<DataResult<List<Role>>> GetAll(string token);
        Task<DataResult<Role>> Get(string token, int id);
    }
}
