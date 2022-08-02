using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlStudentRepository : BaseRepository, IStudentRepository
    {
        public SqlStudentRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Student value)
        {
            using NpgsqlConnection connection = new(connectionString);
            connection.Open();
            string cmdString =
                "Insert Into Students(UserId,AcceptanceDate,SpecialtyId,GroupId) Values(@userId,@acceptanceDate,@specialtyId,@groupId)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@userId", value.User.Id);
            command.Parameters.AddWithValue("@acceptanceDate", value.AcceptanceDate);
            command.Parameters.AddWithValue("@specialtyId", value.Specialty.Id);
            command.Parameters.AddWithValue("@groupId", value.Group.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(connectionString);
            connection.Open();
            string cmdString = "Delete From Students Where Id = @id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Student Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Student> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(Student value)
        {
            throw new NotImplementedException();
        }

        private Student ReadStudent(NpgsqlDataReader reader)
        {
            return new Student
            {
                Id = reader.Get<int>("StudentId"),
                AcceptanceDate = reader.Get<DateTime>("AcceptanceDate"),
                Group = new Group
                {
                    Id = reader.Get<int>("GroupId"),
                    Name = reader.Get<string>("GroupName"),
                    Sector = new Sector
                    {
                        Id = reader.Get<int>("SectorId"),
                        Name = reader.Get<string>("SectorName")
                    },
                    Speciality = new Specialty
                    {
                        Id = reader.Get<int>("SpecialtyId"),
                        Name = reader.Get<string>("SpecialtyName"),
                        Faculty = new Faculty
                        {
                            Id = reader.Get<int>("FacultyId"),
                            Name = reader.Get<string>("FacultyName")
                        }
                    }
                },
                Specialty = new Specialty
                {
                    Id = reader.Get<int>("StudentSpecialtyName"),
                    Name = reader.Get<string>("StudentSpecialtyName"),
                    Faculty = new Faculty
                    {
                        Id = reader.Get<int>("StudentSpecialtyFacultyId"),
                        Name = reader.Get<string>("StudentSpecialtyName")
                    }
                },
                User = new User
                {
                    Id = reader.Get<int>("UserId"),
                    BirthDate = reader.Get<DateTime>("BirthDate"),
                    FatherName = reader.Get<string>("FatherName"),
                    FirstName = reader.Get<string>("FirstName"),
                    LastName = reader.Get<string>("LastName"),
                    LastModified = reader.Get<DateTime>("UserLastModified"),
                    Gender = reader.Get<bool>("Gender"),
                    IsDeleted = reader.Get<bool>("UserIsDeleted")
                }
            };
        }
    }
}
