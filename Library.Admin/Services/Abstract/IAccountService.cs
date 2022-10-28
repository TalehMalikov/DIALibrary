using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Services.Abstract
{
    public interface IAccountService 
    {
        Task<DataResult<List<Account>>> GetAll(string token); 
        Task<DataResult<Account>> Get(string token, int id);
        Task<DataResult<Account>> GetByEmail(string name);
        Task<Result> Add(string token, AccountDto entity);
        Task<Result> Update(string token, AccountDto entity);
        Task<Result> Delete(string token, int id);

    }
}
