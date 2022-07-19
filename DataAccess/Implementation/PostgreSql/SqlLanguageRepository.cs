
using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlLanguageRepository : BaseRepository, ILanguageRepository
    {
        public SqlLanguageRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Language value)
        {

            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string cmdString = "Insert Into Languages(Name) Values(@name)";
            connection.Open();
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@name", value.Name);
            return 1 == cmd.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string cmdString = "Delete From Languages Where Id = @id";
            connection.Open();
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            return 1 == command.ExecuteNonQuery();
        }

        public Language Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Languages Where Id = @id";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadLanguage(reader);
            return null;
        }

        public List<Language> GetAll()
        {
            List<Language> languages = new List<Language>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Languages";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                languages.Add(ReadLanguage(reader));
            return languages;
        }

        public bool Update(Language value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            string cmdString = "Update Languages Set Name=@name Where id=@id";
            connection.Open();
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Language ReadLanguage(NpgsqlDataReader reader)
        {
            return new Language
            {
                Id = reader.Get<int>("Id"),
                Name = reader.Get<string>("Name")
            };
        }
    }
}
