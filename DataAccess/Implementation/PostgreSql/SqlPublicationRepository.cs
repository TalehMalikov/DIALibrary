using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlPublicationRepository : IPublicationRepository
    {
        private readonly string _connectionString;
        public SqlPublicationRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Publication value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Insert Into Publications(BookId,PhotoPath,FilePath,PublisherName,PageNumber,PublicationLanguageId,PublicationDate,LastModified,IsDeleted) " +
                "Values(@bookId,@photoPath,@filePath,@publisherName,@pageNumber,@publicationLanguageId,@publicationDate,@lastModified,@isDeleted)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@bookId", value.Book.Id);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@publisherName", value.PublisherName);
            command.Parameters.AddWithValue("@pageNumber", value.PageNumber);
            command.Parameters.AddWithValue("@publicationLanguageId",value.PublicationLanguage);
            command.Parameters.AddWithValue("@publicationDate", value.PublicationDate);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", false);
            return 1 == command.ExecuteNonQuery();

        }

        public bool Delete(int id)
        {
            using NpgsqlConnection npgsqlConnection = new(_connectionString);
            npgsqlConnection.Open();
            string cmdString = "Delete From Publications Where Id=@id";
            using NpgsqlCommand command = new(cmdString,npgsqlConnection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Publication Get(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select Publications.Id as PublicationId, " +
                               "Books.Id as BookId,Books.Name as BookName, Categories.Id as CategoryId, " +
                               "Categories.Name as CategoryName,Languages.Id as BookLanguageId, " +
                               "Languages.Name as BookLanguageName, " +
                               "PhotoPath,FilePath,PublisherName,PageNumber, " +
                               "Languages.Id as PublicationLanguageId, Languages.Name as PublicationLanguageName, " +
                               "PublicationDate, Publications.LastModified, Publications.IsDeleted From Publications " +
                               "inner join Books on Publications.BookId = Books.Id " +
                               "inner join Languages on Books.OriginalLanguageId = Languages.Id and Publications.PublicationLanguageId = Languages.Id " +
                               "inner join Categories on Books.CategoryId = Categories.Id Where Publications.Id =@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id");
            var reader = command.ExecuteReader();
            if(reader.Read())
                return ReadPublication(reader);
            return null;
        }

        public List<Publication> GetAll()
        {
            List<Publication> list = new List<Publication>();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select Publications.Id as PublicationId, " +
                               "Books.Id as BookId,Books.Name as BookName, Categories.Id as CategoryId, " +
                               "Categories.Name as CategoryName,Languages.Id as BookLanguageId, " +
                               "Languages.Name as BookLanguageName, " +
                               "PhotoPath,FilePath,PublisherName,PageNumber, " +
                               "Languages.Id as PublicationLanguageId, Languages.Name as PublicationLanguageName, " +
                               "PublicationDate, Publications.LastModified, Publications.IsDeleted From Publications " +
                               "inner join Books on Publications.BookId = Books.Id " +
                               "inner join Languages on Books.OriginalLanguageId = Languages.Id and Publications.PublicationLanguageId = Languages.Id " +
                               "inner join Categories on Books.CategoryId = Categories.Id ";
            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                 list.Add(ReadPublication(reader));
            return list;
        }

        public bool Update(Publication value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Update Publications Set BookId=@bookId,PhotoPath=@photoPath,FilePath=@filePath,PublisherName=@publisherName,PageNumber=@pageNumber," +
                "PublicationLanguageId=@publicationLanguageId,PublicationDate=@publicationDate,LastModified=@lastModified,IsDeleted=@isDeleted Where Publications.Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@bookId", value.Book.Id);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@publisherName", value.PublisherName);
            command.Parameters.AddWithValue("@pageNumber", value.PageNumber);
            command.Parameters.AddWithValue("@publicationLanguageId", value.PublicationLanguage);
            command.Parameters.AddWithValue("@publicationDate", value.PublicationDate);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@isDeleted", false);
            command.Parameters.AddWithValue("@id",value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Publication ReadPublication(NpgsqlDataReader reader)
        {
            return new Publication
            {
                Id = reader.Get<int>("PublicationId"),
                Book = new Book
                {
                    Id = reader.Get<int>("BookId"),
                    Category = new Category
                    {
                        Id = reader.Get<int>("CategoryId"),
                        Name = reader.Get<string>("CategoryName"),
                    },
                    Name = reader.Get<string>("BookName"),
                    OriginalLanguage = new Language
                    {
                        Id = reader.Get<int>("LanguageId"),
                        Name = reader.Get<string>("LanguageName")
                    },
                    LastModified = reader.Get<DateTime>("BookLastModified"),
                    IsDeleted = reader.Get<bool>("BookIsDeleted"),
                },
                FilePath = reader.Get<string>("FilePath"),
                PhotoPath = reader.Get<string>("PhotoPath"),
                IsDeleted = reader.Get<bool>("PublicationIsDeleted"),
                LastModified = reader.Get<DateTime>("PublicationLastModified"),
                PageNumber = reader.Get<short>("PageNumber"),
                PublicationDate = reader.Get<DateTime>("PublicationDate"),
                PublicationLanguage = new Language
                {
                    Id = reader.Get<int>("PublicationLanguageId"),
                    Name = reader.Get<string>("PublicationLanguageName")
                },
                PublisherName = reader.Get<string>("PublisherName")
            };
        }
    }
}
