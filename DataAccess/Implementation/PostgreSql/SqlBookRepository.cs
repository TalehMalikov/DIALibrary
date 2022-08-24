using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlBookRepository : IBookRepository
    {
        private readonly string _connectionString;
        public SqlBookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Entities.Concrete.File value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into Books(Name,CategoryId,OriginalLanguageId,LastModified,IsDeleted,Status,FileTypeId)" +
                               " Values(@name,@categoryId,@originalCategoryId,@lastModified,@isDeleted,@status,@fileTypeId)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.Category.Id);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLanguage.Id);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", false);
            command.Parameters.AddWithValue("@status", value.Status);
            command.Parameters.AddWithValue("@fileTypeId", value.Type.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Update Books Set IsDeleted=@isDeleted Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            command.Parameters.AddWithValue("@isDeleted", true);
            return 1 == command.ExecuteNonQuery();
        }

        public Entities.Concrete.File Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = " select Categories.Id as CategoryId, Categories.Name as CategoryName,Books.Id as BookId, Books.Name as BookName," +
            "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName, " +
            "FileTypes.Id as FileTypeId, FileTypes.Name as FileTypeName" +
            "from Books inner join Categories on Books.CategoryId = Categories.Id inner join Languages on Books.OriginalLanguageId = Languages.Id " +
            "inner join FileTypes on Books.FileTypeId=FileTypes.Id" + 
            "Where Books.Id=@id and IsDeleted=false";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadBook(reader);
            return null;
        }

        public List<Entities.Concrete.File> GetAll()
        {
            List<Entities.Concrete.File> books = new List<Entities.Concrete.File>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select Categories.Id as CategoryId, Categories.Name as CategoryName,Books.Id as BookId, Books.Name as BookName," + 
                               "Books.LastModified as LastModified, Books.IsDeleted as IsDeleted, Languages.Id as LanguageId,Languages.Name as OriginalLanguageName " +
                               "from Books inner join Categories on Books.CategoryId = Categories.Id inner join Languages on Books.OriginalLanguageId = Languages.Id " +
                               "Where Books.IsDeleted = false";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                books.Add(ReadBook(reader));
            return books;
        }

        public bool Update(Entities.Concrete.File value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
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

        private Entities.Concrete.File ReadBook(NpgsqlDataReader reader)
        {
            return new Entities.Concrete.File
            {
                Id = reader.Get<int>("BookId"),
                Category = new Category
                {
                    Id = reader.Get<int>("CategoryId"),
                    Name = reader.Get<string>("CategoryName")
                },
                OriginalLanguage = new Language
                {
                    Id = reader.Get<int>("LanguageId"),
                    Name = reader.Get<string>("LanguageName")
                },
                IsDeleted = reader.Get<bool>("IsDeleted"),
                LastModified = reader.Get<DateTime>("LastModified"),
                Name = reader.Get<string>("BookName"),
                Status = reader.Get<bool>("BookStatus"),
                Type = new FileType
                {
                    Id = reader.Get<int>("FileTypeId"),
                    Name = reader.Get<string>("FileTypeName")
                }
            };
        }
    }
}
