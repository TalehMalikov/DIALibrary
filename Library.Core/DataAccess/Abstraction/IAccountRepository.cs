using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Abstraction
{
    public interface IAccountRepository : ICrudRepository<Account>
    {
    }
}
