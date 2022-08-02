using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAccountRoleRepository : BaseRepository,IAccountRoleRepository
    {
        public SqlAccountRoleRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(AccountRole value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AccountRole Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountRole value)
        {
            throw new NotImplementedException();
        }
    }
}
