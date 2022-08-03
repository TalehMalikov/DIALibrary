using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlPublicationRepository : BaseRepository, IPublicationRepository
    {
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
