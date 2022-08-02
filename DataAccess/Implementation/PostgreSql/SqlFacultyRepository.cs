using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlFacultyRepository : BaseRepository, IFacultyRepository
    {
        public SqlFacultyRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Faculty value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Faculties(Name) Values(@name)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Delete From Faculties Where Id=@id";
            NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Faculty Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Faculties Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadFaculty(reader);
            return null;
        }

        public List<Faculty> GetAll()
        {
            List<Faculty> facultyList = new List<Faculty>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Faculties";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                facultyList.Add(ReadFaculty(reader));
            return facultyList;
        }

        public bool Update(Faculty value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Faculties Set Name=@name Where Id=@id";
            NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1 == command.ExecuteNonQuery();
        }

        private Faculty ReadFaculty(NpgsqlDataReader reader)
        {
            return new Faculty
            {
                Id = reader.Get<int>(nameof(Faculty.Id)),
                Name = reader.Get<string>(nameof(Faculty.Name))
            };
        }
    }
}
