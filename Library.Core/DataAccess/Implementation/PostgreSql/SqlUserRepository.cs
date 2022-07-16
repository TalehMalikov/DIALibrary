using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlUserRepository : BaseRepository, IUserRepository
    {
        public SqlUserRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(User value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(User value)
        {
            throw new NotImplementedException();
        }
    }
}
