using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlFileTypeRepository : IFileTypeRepository
    {
        private readonly string _connectionString;
        public SqlFileTypeRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(FileType value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Insert Into FileTypes(Name) Values(@name)";
            using NpgsqlCommand command = new(cmdString,connection);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1 == command.ExecuteNonQuery();
        }

        public FileType Get(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select * From FileTypes Where Id=@id";
            NpgsqlCommand command = new(cmdString,connection);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadFileType(reader);
            return null;
        }

        public List<FileType> GetAll()
        {
            List<FileType> fileTypes = new();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select * From FileTypes";
            NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while(reader.Read())
                 fileTypes.Add(ReadFileType(reader));
            return fileTypes;
        }

        public bool Update(FileType value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Update Table FileTypes Set Name=@name Where Id = @id";
            NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Delete FileTypes Where Id=@id";
            NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        private FileType ReadFileType(NpgsqlDataReader reader)
        {
            return new FileType
            {
                Id = reader.Get<int>(nameof(FileType.Id)),
                Name = reader.Get<string>(nameof(FileType.Name))
            };
        }
    }
}
