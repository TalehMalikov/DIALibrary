using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlBookAuthorRepository : IBookAuthorRepository
    {
        private readonly string _connectionString;
        public SqlBookAuthorRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(BookAuthor value)
        {
            using NpgsqlConnection  connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into BookAuthors(BookId,AuthorId) Values(@bookId,@authorId)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@bookId", value.Book.Id);
            command.Parameters.AddWithValue("@authorId", value.Author.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Delete From BookAuthors Where Id=@id";
            using NpgsqlCommand command = new(cmdString,connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command. ExecuteNonQuery();
        }

        public BookAuthor Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "select BookAuthors.Id,Books.Id as BookId,Books.Name as BookName, Books.LastModified,Books.IsDeleted," +
                " Authors.Id as AuthorId,Authors.FirstName,Authors.LastName,Authors.FatherName,Authors.BookCount,Authors.Gender," +
                " Languages.Id as LanguageId, Languages.Name as LanguageName," +
                "Categories.Id as CategoryId, Categories.Name as CategoryName from BookAuthors " +
                "inner join Books on BookAuthors.BookId = Books.Id " +
                "inner join Authors on BookAuthors.AuthorId = Authors.Id " +
                "inner join Categories on Books.CategoryId = Categories.Id " +
                "inner join Languages on Books.OriginalLanguageId = Languages.Id Where BookAuthors.Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadBookAuthor(reader);
            return null;

        }

        public List<BookAuthor> GetAll()
        {
            List<BookAuthor> bookAuthorList = new List<BookAuthor>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "select BookAuthors.Id,Books.Id as BookId,Books.Name as BookName, Books.LastModified,Books.IsDeleted," +
                " Authors.Id as AuthorId,Authors.FirstName,Authors.LastName,Authors.FatherName,Authors.BookCount,Authors.Gender," +
                " Languages.Id as LanguageId, Languages.Name as LanguageName," +
                "Categories.Id as CategoryId, Categories.Name as CategoryName from BookAuthors " +
                "inner join Books on BookAuthors.BookId = Books.Id " +
                "inner join Authors on BookAuthors.AuthorId = Authors.Id " +
                "inner join Categories on Books.CategoryId = Categories.Id " +
                "inner join Languages on Books.OriginalLanguageId = Languages.Id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                bookAuthorList.Add(ReadBookAuthor(reader));
            return bookAuthorList;
        }

        public bool Update(BookAuthor value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Update BookAuthors Set BookId=@bookId, AuthorId=@authorId Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@bookId", value.Book.Id);
            command.Parameters.AddWithValue("@authorId", value.Author.Id);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private BookAuthor ReadBookAuthor(NpgsqlDataReader reader)
        {
            return new BookAuthor
            {
                Id = reader.Get<int>("BookAuthorId"),
                Author = new Author
                {
                    Id = reader.Get<int>("AuthorId"),
                    BookCount = reader.Get<short>("BookCount"),
                    FirstName = reader.Get<string>("FirstName"),
                    FatherName = reader.Get<string>("FatherName"),
                    LastName = reader.Get<string>("LastName"),
                    Gender = reader.Get<bool>("Gender")
                },
                Book = new Book
                {
                    Id = reader.Get<int>("BookId"),
                    Name = reader.Get<string>("BookName"),
                    Category = new Category
                    {
                        Id = reader.Get<int>("CategoryId"),
                        Name = reader.Get<string>("CategoryName")
                    },
                    IsDeleted = reader.Get<bool>("IsDeleted"),
                    LastModified = reader.Get<DateTime>("LastModified"),
                    OriginalLanguage = new Language
                    {
                        Id = reader.Get<int>("LanguageId"),
                        Name = reader.Get<string>("LanguageName")
                    }
                }

            };
        }
    }
}
