using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAuthorRepository : BaseRepository, IAuthorRepository
    {
        public SqlAuthorRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Author value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdSTring = "Insert Into Authors(FirstName,LastName,FatherName,BookCount,Gender) " +
                               "Values(@firstName,@lastName,@fatherName,@bookCount,@gender)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdSTring, connection);
            command.Parameters.AddWithValue("@firstName", value.FirstName);
            command.Parameters.AddWithValue("@lastName", value.LastName);
            command.Parameters.AddWithValue("@fatherName", value.FatherName);
            command.Parameters.AddWithValue("@bookCount", value.BookCount);
            command.Parameters.AddWithValue("@gender", value.Gender);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdSTring = "Delete From Authors Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdSTring, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Author Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdSTring = "Select * From Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdSTring, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadAuthor(reader);
            return null;
        }

        public List<Author> GetAll()
        {
            List<Author> authors = new List<Author>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdSTring = "Select * From Where";
            using NpgsqlCommand command = new NpgsqlCommand(cmdSTring, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                authors.Add(ReadAuthor(reader));
            return authors;
        }

        public bool Update(Author value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdSTring = "Update Authors Set FirstName=@firstName,LastName=@lastName," +
                               "FatherName=@fatherName,BookCount=@bookCount,Gender=@gender Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdSTring, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@firstName", value.FirstName);
            command.Parameters.AddWithValue("@lastName", value.LastName);
            command.Parameters.AddWithValue("@fatherName", value.FatherName);
            command.Parameters.AddWithValue("@bookCount", value.BookCount);
            command.Parameters.AddWithValue("@gender", value.Gender);
            return 1 == command.ExecuteNonQuery();
        }

        private Author ReadAuthor(NpgsqlDataReader reader)
        {
            return new Author
            {
                Id = reader.Get<int>(nameof(Author.Id)),
                FirstName = reader.Get<string>(nameof(Author.FirstName)),
                LastName = reader.Get<string>(nameof(Author.LastName)),
                FatherName = reader.Get<string>(nameof(Author.FatherName)),
                BookCount = reader.Get<short>(nameof(Author.BookCount)),
                Gender = reader.Get<bool>(nameof(Author.Gender))
            };
        }
    }
}
