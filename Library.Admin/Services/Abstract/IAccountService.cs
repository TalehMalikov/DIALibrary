using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.Admin.Services.Abstract
{
    public interface IAccountService
    {
        Task<Result> Add(string token,Account account);
        Task<DataResult<List<Account>>> GetAll(string token);
    }
}
