using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAccountRoleRepository : BaseRepository, IAccountRoleRepository
    {
        public SqlAccountRoleRepository(string connectionString) : base(connectionString)
        {
        }
        
        public bool Add(Account value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Account> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Account value)
        {
            throw new NotImplementedException();
        }
    }
}
