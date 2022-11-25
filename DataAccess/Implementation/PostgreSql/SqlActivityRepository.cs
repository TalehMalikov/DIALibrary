using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlActivityRepository : IActivityRepository
    {
        private readonly string _connectionString;
        public SqlActivityRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Activity value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Insert Into Activities(Title,Content,PhotoPath,CreatedDate,IsDeleted,LastModified) Values(@title,@content,@photoPath,@createdDate,@isDeleted,@lastModified)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@title", value.Title);
            command.Parameters.AddWithValue("@content", value.Content);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@createdDate", value.CreatedDate);
            command.Parameters.AddWithValue("@lastModified", DateTime.Now);
            command.Parameters.AddWithValue("@isDeleted", false);
            return 1 == command.ExecuteNonQuery();
        }

        public Activity Get(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select * From Activities Where Id=@id and IsDeleted=false";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadActivity(reader);
            return null;
        }

        public List<Activity> GetAll()
        {
            List<Activity> activities = new();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select * From Activities Where IsDeleted=false order by CreatedDate desc";
            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                activities.Add(ReadActivity(reader));
            return activities;
        }

        public bool Update(Activity value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Update Activities Set Title=@title,Content=@content,PhotoPath=@photoPath,CreatedDate=@createdDate,IsDeleted=@isDeleted,LastModified=@lastModified Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@title", value.Title);
            command.Parameters.AddWithValue("@content", value.Content);
            command.Parameters.AddWithValue("@photoPath", value.PhotoPath);
            command.Parameters.AddWithValue("@createdDate", value.CreatedDate);
            command.Parameters.AddWithValue("@lastModified", DateTime.Now);
            command.Parameters.AddWithValue("@isDeleted", false);
            return 1 == command.ExecuteNonQuery();
        }

        public bool DeleteFromDb(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Delete from Activities Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Activate(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Update activities set isdeleted = false where id=@id";
            using NpgsqlCommand command = new(cmdString,connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public List<Activity> GetDeletedActivities()
        {
            List<Activity> activities = new();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Select * From Activities Where IsDeleted=true order by CreatedDate desc";
            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                activities.Add(ReadActivity(reader));
            return activities;
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Update Activities Set IsDeleted=true Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        private Activity ReadActivity(NpgsqlDataReader reader)
        {
            return new Activity
            {
                Id = reader.Get<int>(nameof(Activity.Id)),
                Content = reader.Get<string>(nameof(Activity.Content)),
                PhotoPath = reader.Get<string>(nameof(Activity.PhotoPath)),
                CreatedDate = reader.Get<DateTime>(nameof(Activity.CreatedDate)),
                Title = reader.Get<string>(nameof(Activity.Title)),
                IsDeleted = reader.Get<bool>(nameof(Activity.IsDeleted)),
                LastModified = reader.Get<DateTime>(nameof(Activity.LastModified)),
            };
        }
    }
}
