using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlBookRepository : BaseRepository, IBookRepository
    {
        public bool Add(Book value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Books(Name,CategoryId,OriginalLanguageId,LastModified,IsDeleted)" +
                               " Values(@name,@categoryId,@originalCategoryId,@lastModified,@isDeleted)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.Category.Id);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLanguage.Id);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", false);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Books Set IsDeleted=@isDeleted Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@isDeleted", true);
            return 1 == command.ExecuteNonQuery();
        }

        public Book Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = " select Categories.Id as CategoryId, Categories.Name as CategoryName,Books.Id as BookId, Books.Name as BookName," +
            "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName, " +
            "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName " +
            "from Books inner join Categories on Books.CategoryId = Categories.Id inner join Languages on Books.OriginalLanguageId = Languages.Id " +
                               "Where Id=@id and IsDeleted=false";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
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
            string cmdString = " select Categories.Id as CategoryId, Categories.Name as CategoryName,Books.Id as BookId, Books.Name as BookName," + 
                               "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName " +
                               "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName " +
                               "from Books inner join Categories on Books.CategoryId = Categories.Id inner join Languages on Books.OriginalLanguageId = Languages.Id ";
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
            command.Parameters.AddWithValue("@categoryId", value.Category.Id);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLanguage.Id);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", value.IsDeleted);
            return 1 == command.ExecuteNonQuery();
        }

        private Book ReadBook(NpgsqlDataReader reader)
        {
            return new Book
            {
                Id = reader.Get<int>(nameof(Book.Id)),
                Category = new Category
                {
                    Id = reader.Get<int>(nameof(Category.Id)),
                    Name = reader.Get<string>(nameof(Category.Name))
                },
                OriginalLanguage = new Language
                {
                    Id = reader.Get<int>(nameof(Language.Id)),
                    Name = reader.Get<string>(nameof(Language.Name))
                },
                IsDeleted = reader.Get<bool>(nameof(Book.IsDeleted)),
                LastModified = reader.Get<DateTime>(nameof(Book.LastModified)),
                Name = reader.Get<string>(nameof(Book.Name))
            };
        }
    }
}
