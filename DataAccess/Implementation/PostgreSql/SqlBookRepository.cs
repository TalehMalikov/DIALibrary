using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlBookRepository : BaseRepository, IBookRepository
    {
        public SqlBookRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Book value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Books(Name,CategoryId,OriginalLanguageId,LastModified,IsDeleted)" +
                               " Values(@name,@categoryId,@originalCategoryId,@lastModified,@isDeleted)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.CategoryId);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLangaugeId);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", false);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Delete From Books Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString,connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Book Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Books Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString,connection);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadBook(reader);
            return null;
        }

        public List<Book> GetAll()
        {
            List<Book> books = new List<Book>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Books";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                 books.Add(ReadBook(reader));
            return books;
        }

        public bool Update(Book value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString =
                "Update Books Set Name=@name,CategoryId=@categoryId,OriginalLanguageId=@originalLanguageId,LastModified=@lastModified,IsDeleted=@isDeleted Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.CategoryId);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLangaugeId);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", value.IsDeleted);
            return 1 == command.ExecuteNonQuery();
        }

        private Book ReadBook(NpgsqlDataReader reader)
        {
            return new Book
            {
                Id = reader.Get<int>(nameof(Book.Id)),
                CategoryId = reader.Get<int>(nameof(Book.CategoryId)),
                OriginalLangaugeId = reader.Get<int>(nameof(Book.OriginalLangaugeId)),
                IsDeleted = reader.Get<bool>(nameof(Book.IsDeleted)),
                LastModified = reader.Get<DateTime>(nameof(Book.LastModified)),
                Name = reader.Get<string>(nameof(Book.Name))
            };
        }
    }
}
