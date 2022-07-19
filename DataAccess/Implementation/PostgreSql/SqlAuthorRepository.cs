using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
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
