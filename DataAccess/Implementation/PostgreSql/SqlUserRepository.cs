using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlUserRepository : BaseRepository, IUserRepository
    {
        public bool Add(User value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString =
                "Insert Into Users(FirstName,LastName,FatherName,BirthDate,Gender,IsDeleted,LastModified) " +
                "Values(@firstName,@lastName,@fatherName,@birthDate,@gender,@isDeleted,@lastModified)";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@firstName", value.FirstName);
            cmd.Parameters.AddWithValue("@lastName", value.LastName);
            cmd.Parameters.AddWithValue("@fatherName", value.FatherName);
            cmd.Parameters.AddWithValue("@birthDate", value.BirthDate);
            cmd.Parameters.AddWithValue("@gender", value.Gender);
            cmd.Parameters.AddWithValue("@isDeleted", false);
            cmd.Parameters.AddWithValue("@lastModified", value.LastModified);
            return 1 == cmd.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Users Set IsDeleted=true Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public User Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Users Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadUser(reader);
            return null;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Users";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                 users.Add(ReadUser(reader));
            return users;
        }

        public bool Update(User value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString =
                "Update Users Set FirstName=@firstName,LastName=@lastName,FatherName=@fatherName,BirthDate=@birthDate,Gender=@gender,IsDeleted=@isDeleted,LastModified=@lastModified " +
                " Where Id=@id";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@id",value.Id);
            cmd.Parameters.AddWithValue("@firstName", value.FirstName);
            cmd.Parameters.AddWithValue("@lastName", value.LastName);
            cmd.Parameters.AddWithValue("@fatherName", value.FatherName);
            cmd.Parameters.AddWithValue("@birthDate", value.BirthDate);
            cmd.Parameters.AddWithValue("@gender", value.Gender);
            cmd.Parameters.AddWithValue("@isDeleted", false);
            cmd.Parameters.AddWithValue("@lastModified", value.LastModified);
            return 1 == cmd.ExecuteNonQuery();
        }

        private User ReadUser(NpgsqlDataReader reader)
        {
            return new User
            {
                Id = reader.Get<int>(nameof(User.Id)),
                FirstName = reader.Get<string>(nameof(User.FirstName)),
                LastName = reader.Get<string>(nameof(User.LastName)),
                FatherName = reader.Get<string>(nameof(User.FatherName)),
                BirthDate = reader.Get<DateTime>(nameof(User.BirthDate)),
                LastModified = reader.Get<DateTime>(nameof(User.LastModified)),
                IsDeleted = reader.Get<bool>(nameof(User.IsDeleted)),
                Gender = reader.Get<bool>(nameof(User.Gender))
            };
        }
    }
}
