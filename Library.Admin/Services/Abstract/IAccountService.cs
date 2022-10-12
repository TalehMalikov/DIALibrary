using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IAccountService : IBaseService<Account>
    {
        Task<DataResult<List<Account>>> GetAll(string token); 
        Task<DataResult<Account>> Get(string token, int id);

    }
}
