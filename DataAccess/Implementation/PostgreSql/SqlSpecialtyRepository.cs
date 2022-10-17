using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{

    public class SqlSpecialtyRepository :  ISpecialtyRepository
    {
        private readonly string _connectionString;
        public SqlSpecialtyRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Add(SpecialtyDto value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Insert Into Specialties(FacultyId,Name) Values(@facultyId,@name)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@facultyId", value.FacultyId);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1 == command.ExecuteNonQuery();
        }
        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Delete From Specialties Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Specialty Get(int id)
        {
            //"from accounts inner join Users on Users.id = accounts.userid where Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Select Faculties.Id as FacultyId,Faculties.Name as FacultyName, Specialties.Id as SpecialtyId," +
                               "Specialties.Name as SpecialtyName From Specialties inner join Faculties on Specialties.FacultyId = Faculties.Id " +
                               "Where Specialties.Id = @specialtyId";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@specialtyId", id);
            dynamic reader = command.ExecuteReader();
            if (reader.Read())
                return ReadSpecialty(reader);
            return null;
        }

        public List<Specialty> GetAll()
        {
            List<Specialty> specialties = new List<Specialty>();
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open(); 
            string cmdString = "Select Faculties.Id as FacultyId,Faculties.Name as FacultyName, Specialties.Id as SpecialtyId, " +
                               "Specialties.Name as SpecialtyName From Specialties inner join Faculties on Specialties.FacultyId = Faculties.Id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            dynamic reader = command.ExecuteReader();
            while (reader.Read())
                 specialties.Add(ReadSpecialty(reader));
            return specialties;
        }

        public bool Update(SpecialtyDto value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdString = "Update Specialties Set FacultyId=@facultyName, Name=@name Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@facultyId", value.FacultyId);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Specialty ReadSpecialty(NpgsqlDataReader reader)
        {
            return new Specialty
            {
                Id = reader.Get<int>("SpecialtyId"),
                Faculty = new Faculty
                {
                    Id = reader.Get<int>("FacultyId"),
                    Name = reader.Get<string>("FacultyName")
                },
                Name = reader.Get<string>("SpecialtyName")
            };
        }
    }
}
