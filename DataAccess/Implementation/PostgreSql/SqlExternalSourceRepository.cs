using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlExternalSourceRepository : IExternalSourceRepository
    {
        private readonly string _connectionString;
        public SqlExternalSourceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(ExternalSource value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into ExternalSources(Name,PhotoPath,SourceLink) Values(@name,@photopath,@sourcelink)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@photopath", value.PhotoPath);
            command.Parameters.AddWithValue("@sourcelink", value.SourceLink);
            return 1 == command.ExecuteNonQuery(); ;
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Delete From ExternalSources Where Id = @id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public ExternalSource Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select * From ExternalSources Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadExternalSource(reader);
            return null;
        }

        public List<ExternalSource> GetAll()
        {
            List<ExternalSource> externalSources = new List<ExternalSource>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select * From ExternalSources";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                externalSources.Add(ReadExternalSource(reader));
            return externalSources;
        }

        public bool Update(ExternalSource value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Update ExternalSource Set Name = @name,PhotoPath=@photopath,SourceLink=@sourcelink Where Id = @id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@photopath", value.PhotoPath);
            command.Parameters.AddWithValue("@sourcelink", value.SourceLink);
            return 1 == command.ExecuteNonQuery();
        }

        private ExternalSource ReadExternalSource(NpgsqlDataReader reader)
        {
            return new ExternalSource
            {
                Id = reader.Get<int>(nameof(ExternalSource.Id)),
                Name = reader.Get<string>(nameof(ExternalSource.Name)),
                PhotoPath = reader.Get<string>(nameof(ExternalSource.PhotoPath)),
                SourceLink = reader.Get<string>(nameof(ExternalSource.SourceLink))
            };
        }
    }
}
