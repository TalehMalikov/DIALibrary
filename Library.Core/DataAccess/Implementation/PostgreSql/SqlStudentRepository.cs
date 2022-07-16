using Library.Core.DataAccess.Abstraction;
using Library.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.DataAccess.Implementation.PostgreSql
{
    public class SqlStudentRepository : BaseRepository, IStudentRepository
    {
        public SqlStudentRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Student value)
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Student value)
        {
            throw new NotImplementedException();
        }
    }
}
