using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlPublicationRepository : BaseRepository, IPublicationRepository
    {
        public SqlPublicationRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Publication value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Publication Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Publication> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Publication value)
        {
            throw new NotImplementedException();
        }
    }
}
