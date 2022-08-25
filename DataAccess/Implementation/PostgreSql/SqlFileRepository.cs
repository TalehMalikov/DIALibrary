using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;
using File = Library.Entities.Concrete.File;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlFileRepository : IFileRepository
    {
        private readonly string _connectionString;
        public SqlFileRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(File value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into Books(Name,CategoryId,OriginalLanguageId,LastModified,IsDeleted,ExistingStatus," +
                               "FileTypeId,EditionStatus,PublicationLanguageId,Description,PublisherName,PublicationDate," +
                               "PhotoPath,FilePath,PageNumber)" +
                               " Values(@name,@categoryId,@originalCategoryId,@lastModified,@isDeleted,@status,@fileTypeId," +
                               "@editionStatus,@publicationLanguageId,@description,@publisherName,@publicationDate,@photoPath," +
                               "@filePath,@pageNumber)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.Category.Id);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLanguage.Id);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@existingStatus", value.ExistingStatus);
            command.Parameters.AddWithValue("@fileTypeId", value.FileType.Id);
            command.Parameters.AddWithValue("@editionStatus", value.EditionStatus);
            command.Parameters.AddWithValue("@publicationLanguageId", value.PublicationLanguage.Id);
            command.Parameters.AddWithValue("@description", value.Description);
            command.Parameters.AddWithValue("@publisherName", value.PublisherName);
            command.Parameters.AddWithValue("@publicationDate", value.PublicationDate);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@filePath", value.FilePath);
            command.Parameters.AddWithValue("@pageNumber", value.PageNumber);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "delete from Files Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public List<File> GetNewAdded()
        {
            List<File> books = new List<File>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "select files.id as fileid, files.name as FileName,categoryid," +
                               "categories.name as categoryname," +
                               "originallanguageid,ol.name as originallanguagename," +
                               "files.lastmodified as filelastmodified,editionstatus," +
                               "filetypeid,filetypes.name as filetypename," +
                               "photopath,publishername,pagenumber," +
                               "publicationlanguageid, pl.name as publicationlanguagename," +
                               "publicationdate, description,filepath,existingstatus from files " +
                               "inner join categories on categories.id = categoryid " +
                               "inner join languages as ol on originallanguageid = ol.id " +
                               "inner join languages as pl on publicationlanguageid = pl.id " +
                               "inner join filetypes on filetypeid = filetypes.id " +
                               "order by files.lastmodified desc limit 10";
            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while(reader.Read())
                books.Add(ReadFile(reader));
            return books;

        }

        public File Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "select files.id as fileid, files.name as FileName,categoryid," +
                               "categories.name as categoryname," +
                               "originallanguageid,ol.name as originallanguagename," +
                               "files.lastmodified as filelastmodified,editionstatus," +
                               "filetypeid,filetypes.name as filetypename," +
                               "photopath,publishername,pagenumber," +
                               "publicationlanguageid, pl.name as publicationlanguagename," +
                               "publicationdate, description,filepath,existingstatus from files " +
                               "inner join categories on categories.id = categoryid " +
                               "inner join languages as ol on originallanguageid = ol.id " +
                               "inner join languages as pl on publicationlanguageid = pl.id " +
                               "inner join filetypes on filetypeid = filetypes.id " +
                               "where files.id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadFile(reader);
            return null;
        }

        public List<File> GetAll()
        {
            List<File> books = new List<File>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "select files.id as fileid, files.name as FileName,categoryid," +
                               "categories.name as categoryname," +
                               "originallanguageid,ol.name as originallanguagename," +
                               "files.lastmodified as filelastmodified,editionstatus," +
                               "filetypeid,filetypes.name as filetypename," +
                               "photopath,publishername,pagenumber," +
                               "publicationlanguageid, pl.name as publicationlanguagename," +
                               "publicationdate, description,filepath,existingstatus from files " +
                               "inner join categories on categories.id = categoryid " +
                               "inner join languages as ol on originallanguageid = ol.id " +
                               "inner join languages as pl on publicationlanguageid = pl.id " +
                               "inner join filetypes on filetypeid = filetypes.id ";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                books.Add(ReadFile(reader));
            return books;
        }

        public bool Update(File value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString =
                "Update Books Set Name=@name,CategoryId=@categoryId," +
                "OriginalLanguageId=@originalLanguageId,LastModified=@lastModified," +
                "ExistingStatus=@existingStatus,FileTypeId=@fileTypeId,EditionStatus=@editionStatus," +
                "PublicationLanguageId=@publicationLanguageId,Description=@description,PublisherName=@publisherName," +
                "PublicationDate=@publicationDate,PhotoPath=@photoPath,FilePath=@filePath,PageNumber=@pageNumber where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@categoryId", value.Category.Id);
            command.Parameters.AddWithValue("@originalLanguageId", value.OriginalLanguage.Id);
            command.Parameters.AddWithValue("@lastModified", value.LastModified);
            command.Parameters.AddWithValue("@existingStatus", value.ExistingStatus);
            command.Parameters.AddWithValue("@fileTypeId", value.FileType.Id);
            command.Parameters.AddWithValue("@editionStatus", value.EditionStatus);
            command.Parameters.AddWithValue("@publicationLanguageId", value.PublicationLanguage.Id);
            command.Parameters.AddWithValue("@description", value.Description);
            command.Parameters.AddWithValue("@publisherName", value.PublisherName);
            command.Parameters.AddWithValue("@publicationDate", value.PublicationDate);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@filePath", value.FilePath);
            command.Parameters.AddWithValue("@pageNumber", value.PageNumber);
            return 1 == command.ExecuteNonQuery();
        }

        private File ReadFile(NpgsqlDataReader reader)
        {
            return new File
            {
                Id = reader.Get<int>("FileId"),
                Name = reader.Get<string>("FileName"),

                Category = new Category
                {
                    Id = reader.Get<int>("CategoryId"),
                    Name = reader.Get<string>("CategoryName")
                },
                OriginalLanguage = new Language
                {
                    Id = reader.Get<int>("OriginalLanguageId"),
                    Name = reader.Get<string>("OriginalLanguageName")
                },
                FileType=new FileType
                {
                    Id= reader.Get<int>("FileTypeId"),
                    Name=reader.Get<string>("FileTypeName")
                },
                PublicationLanguage=new Language
                {
                    Id = reader.Get<int>("PublicationLanguageId"),
                    Name = reader.Get<string>("PublicationLanguageName")
                },
                PublicationDate =reader.Get<DateTime>("PublicationDate"),
                Description=reader.Get<string>("Description"),
                PhotoPath= reader.Get<string>("PhotoPath"),
                FilePath = reader.Get<string>("FilePath"),
                PublisherName= reader.Get<string>("PublisherName"),
                PageNumber=reader.Get<int>("PageNumber"),
                EditionStatus = reader.Get<bool>("EditionStatus"),
                ExistingStatus=reader.Get<bool>("ExistingStatus"),
                LastModified = reader.Get<DateTime>("FileLastModified")
            };
        }
    }
}
