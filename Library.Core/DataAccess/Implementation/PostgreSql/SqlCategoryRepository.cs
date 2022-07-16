using Library.Core.DataAccess.Abstraction;
using Library.Core.DataAccess.Implementation.PostgreSql;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation
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
