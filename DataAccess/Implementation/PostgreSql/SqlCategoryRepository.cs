using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlCategoryRepository : BaseRepository, ICategoryRepository
    {
        public SqlCategoryRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Category value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Category Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Category> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Category value)
        {
            throw new NotImplementedException();
        }
    }
}
