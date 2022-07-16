using Library.Core.DataAccess.Abstraction;
using Library.Core.DataAccess.Implementation.PostgreSql;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreServer
{
    public class SqlBookRepository : BaseRepository, IBookRepository
    {
        public SqlBookRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Book value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Book Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Book> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Book value)
        {
            throw new NotImplementedException();
        }
    }
}
