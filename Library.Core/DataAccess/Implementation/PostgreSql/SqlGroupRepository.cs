using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlGroupRepository : BaseRepository, IGroupRepository
    {
        public SqlGroupRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Group value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Group Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Group value)
        {
            throw new NotImplementedException();
        }
    }
}
