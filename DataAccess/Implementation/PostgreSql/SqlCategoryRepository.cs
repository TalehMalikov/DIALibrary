using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlCategoryRepository : ICategoryRepository
    {
        private readonly string _connectionString;
        public SqlCategoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(Category value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into Categories(Name) Values(@name)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1==command.ExecuteNonQuery();

        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Delete From Categories Where Id = @id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Category Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select * From Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadCategory(reader);
            return null;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select * From Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                 categories.Add(ReadCategory(reader));
            return categories;
        }

        public bool Update(Category value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Update Category Set Name = @name Where Id = @id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Category ReadCategory(NpgsqlDataReader reader)
        {
            return new Category
            {
                Id = reader.Get<int>(nameof(Category.Id)),
                Name = reader.Get<string>(nameof(Category.Name))
            };
        }
    }
}
