using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.DataAccess.Implementation.PostgreSql
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
