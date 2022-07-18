using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlSectorRepository : BaseRepository, ISectorRepository
    {
        public SqlSectorRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Sector value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Sector Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Sector> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Sector value)
        {
            throw new NotImplementedException();
        }
    }
}
