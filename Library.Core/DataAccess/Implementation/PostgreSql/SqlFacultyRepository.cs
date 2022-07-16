using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlFacultyRepository : BaseRepository, IFacultyRepository
    {
        public SqlFacultyRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Faculty value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Faculty Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Faculty> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Faculty value)
        {
            throw new NotImplementedException();
        }
    }
}
