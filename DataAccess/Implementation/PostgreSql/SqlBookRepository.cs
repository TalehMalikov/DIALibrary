using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
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
