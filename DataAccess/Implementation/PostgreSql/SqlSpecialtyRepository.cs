using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlSpecialtyRepository : BaseRepository,ISpecialtyRepository
    {
        public SqlSpecialtyRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Specialty value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Specialty Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Specialty> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Specialty value)
        {
            throw new NotImplementedException();
        }
    }
}
