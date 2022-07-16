using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlLanguageRepository : BaseRepository, ILanguageRepository
    {
        public SqlLanguageRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Language value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Language Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Language> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Language value)
        {
            throw new NotImplementedException();
        }
    }
}
