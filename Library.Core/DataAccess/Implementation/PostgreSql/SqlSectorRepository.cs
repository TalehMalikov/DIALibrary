using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.DataAccess.Implementation.PostgreSql
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
