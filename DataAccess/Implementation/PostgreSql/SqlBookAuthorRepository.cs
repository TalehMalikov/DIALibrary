using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlBookAuthorRepository : BaseRepository, IBookAuthorRepository
    {
        public SqlBookAuthorRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(BookAuthor value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public BookAuthor Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<BookAuthor> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(BookAuthor value)
        {
            throw new NotImplementedException();
        }
    }
}
