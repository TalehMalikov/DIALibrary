using Library.Entities.Concrete;

namespace Library.DataAccess.Abstraction
{
    public interface IAccountRepository : ICrudRepository<Account>
    {
    }
}
