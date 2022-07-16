using Library.Core.DataAccess.Abstraction;
using Library.Core.DataAccess.Implementation.PostgreSql;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreServer
{
    public class SqlAuthorRepository : BaseRepository, IAuthorRepository
    {
        public SqlAuthorRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Author value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Author Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Author> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Author value)
        {
            throw new NotImplementedException();
        }
    }
}
