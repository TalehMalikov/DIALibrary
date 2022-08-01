using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlSpecialtyRepository : BaseRepository, ISpecialtyRepository
    {
        public SqlSpecialtyRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Specialty value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Specialities(FacultyId,Name) Values(@facultyId,@name)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@facultyId", value.Faculty.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            return 1 == command.ExecuteNonQuery();
        }
        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Delete From Specialities Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Specialty Get(int id)
        {
            //"from accounts inner join Users on Users.id = accounts.userid where Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select Faculties.Id as FacultyId,Faculties.Name as FacultyName, Specialities.Id as SpecialityId," +
                               "Specialities.Name as SpecialityName From Specialities inner join Faculties on Specialities.FacultyId = Faculties.Id " +
                               "Where Specialities.Id = @specialityId";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@specialityId", id);
            dynamic reader = command.ExecuteReader();
            if (reader.Read())
                return ReadSpeciality(reader);
            return null;
        }

        public List<Specialty> GetAll()
        {
            List<Specialty> specialties = new List<Specialty>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select Faculties.Id as FacultyId,Faculties.Name as FacultyName, Specialities.Id as SpecialityId, " +
                               "Specialities.Name as SpecialityName From Specialities inner join Faculties on Specialities.FacultyId = Faculties.Id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            dynamic reader = command.ExecuteReader();
            while (reader.Read())
                specialties.Add(ReadSpeciality(reader));
            return specialties;
        }

        public bool Update(Specialty value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Specialities Set FacultyId=@facultyName, Name=@name Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@facultyId", value.Faculty.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Specialty ReadSpeciality(NpgsqlDataReader reader)
        {
            return new Specialty
            {
                Id = reader.Get<int>("SpecialityId"),
                Faculty = new Faculty
                {
                    Id = reader.Get<int>("FacultyId"),
                    Name = reader.Get<string>("FacultyName")
                },
                Name = reader.Get<string>("SpecialityName")
            };
        }
    }
}
