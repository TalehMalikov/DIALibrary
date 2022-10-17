using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlStudentRepository : IStudentRepository
    {
        private readonly string _connectionString;
        public SqlStudentRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(StudentDto value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Insert Into Students(UserId,AcceptanceDate,SpecialtyId,GroupId) Values(@userId,@acceptanceDate,@specialtyId,@groupId)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@userId", value.UserId);
            command.Parameters.AddWithValue("@acceptanceDate", value.AcceptanceDate);
            command.Parameters.AddWithValue("@specialtyId", value.SpecialtyId);
            command.Parameters.AddWithValue("@groupId", value.GroupId);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Delete From Students Where Id = @id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Student Get(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =  "select Students.Id as StudentId," +
                                "Users.Id as UserId, Users.FirstName,Users.LastName,Users.FatherName,Users.Birthdate,Users.Gender," +
                                "Users.IsDeleted as UserIsDeleted, Users.LastModified as UserLastModified,Students.AcceptanceDate," +
                                "studentsspecialties.Id as StudentSpecialtyId,studentsspecialties.Name as StudentSpecialtyName," +
                                "groupspecialtiefaculties.Id as GroupSpecialtyFacultyId, groupspecialtiefaculties.Name as GroupSpecialtyFacultyName," +
                                "studentsspecialtiefaculties.Id as StudentSpecialtyFacultyId, studentsspecialtiefaculties.Name as StudentSpecialtyFacultyName," +
                                "Groups.Id as GroupId, Groups.Name as GroupName," +
                                "Sectors.Id as SectorId, Sectors.Name as SectorName," +
                                "groupspecialties.Id as GroupSpecialtyId,groupspecialties.Name as GroupSpecialtyName from Students " +
                                "inner join Groups on Students.GroupId = Groups.Id " +
                                "inner join Users on Students.UserId = Users.Id " +
                                "inner join Specialties as studentsspecialties on Students.SpecialtyId = studentsspecialties.Id " +
                                "inner join Specialties as groupspecialties on Groups.SpecialtyId = groupspecialties.Id " +
                                "inner join Sectors on Groups.SectorId = Sectors.Id " +
                                "inner join Faculties as groupspecialtiefaculties  on groupspecialties.FacultyId = groupspecialtiefaculties.Id " +
                                "inner join Faculties as studentsspecialtiefaculties on studentsspecialties.FacultyId = studentsspecialtiefaculties.Id  where Users.IsDeleted=false and Students.Id = @id";

            using NpgsqlCommand command = new(cmdString,connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadStudent(reader);
            return null;
        }

        public Student GetByUserId(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "select Students.Id as StudentId," +
                "Users.Id as UserId, Users.FirstName,Users.LastName,Users.FatherName,Users.Birthdate,Users.Gender," +
                "Users.IsDeleted as UserIsDeleted, Users.LastModified as UserLastModified,Students.AcceptanceDate," +
                "studentsspecialties.Id as StudentSpecialtyId,studentsspecialties.Name as StudentSpecialtyName," +
                "groupspecialtiefaculties.Id as GroupSpecialtyFacultyId, groupspecialtiefaculties.Name as GroupSpecialtyFacultyName," +
                "studentsspecialtiefaculties.Id as StudentSpecialtyFacultyId, studentsspecialtiefaculties.Name as StudentSpecialtyFacultyName," +
                "Groups.Id as GroupId, Groups.Name as GroupName," +
                "Sectors.Id as SectorId, Sectors.Name as SectorName," +
                "groupspecialties.Id as GroupSpecialtyId,groupspecialties.Name as GroupSpecialtyName from Students " +
                "inner join Groups on Students.GroupId = Groups.Id " +
                "inner join Users on Students.UserId = Users.Id " +
                "inner join Specialties as studentsspecialties on Students.SpecialtyId = studentsspecialties.Id " +
                "inner join Specialties as groupspecialties on Groups.SpecialtyId = groupspecialties.Id " +
                "inner join Sectors on Groups.SectorId = Sectors.Id " +
                "inner join Faculties as groupspecialtiefaculties  on groupspecialties.FacultyId = groupspecialtiefaculties.Id " +
                "inner join Faculties as studentsspecialtiefaculties on studentsspecialties.FacultyId = studentsspecialtiefaculties.Id  where Users.IsDeleted=false and Users.Id = @id";

            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadStudent(reader);
            return null;
        }

        public List<Student> GetAll()
        {
            List<Student> students = new List<Student>();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "select Students.Id as StudentId," +
                "Users.Id as UserId, Users.FirstName,Users.LastName,Users.FatherName,Users.Birthdate,Users.Gender," +
                "Users.IsDeleted as UserIsDeleted, Users.LastModified as UserLastModified,Students.AcceptanceDate," +
                "studentsspecialties.Id as StudentSpecialtyId,studentsspecialties.Name as StudentSpecialtyName," +
                "groupspecialtiefaculties.Id as GroupSpecialtyFacultyId, groupspecialtiefaculties.Name as GroupSpecialtyFacultyName," +
                "studentsspecialtiefaculties.Id as StudentSpecialtyFacultyId, studentsspecialtiefaculties.Name as StudentSpecialtyFacultyName," +
                "Groups.Id as GroupId, Groups.Name as GroupName," +
                "Sectors.Id as SectorId, Sectors.Name as SectorName," +
                "groupspecialties.Id as GroupSpecialtyId,groupspecialties.Name as GroupSpecialtyName from Students " +
                "inner join Groups on Students.GroupId = Groups.Id " +
                "inner join Users on Students.UserId = Users.Id " +
                "inner join Specialties as studentsspecialties on Students.SpecialtyId = studentsspecialties.Id " +
                "inner join Specialties as groupspecialties on Groups.SpecialtyId = groupspecialties.Id " +
                "inner join Sectors on Groups.SectorId = Sectors.Id " +
                "inner join Faculties as groupspecialtiefaculties  on groupspecialties.FacultyId = groupspecialtiefaculties.Id " +
                "inner join Faculties as studentsspecialtiefaculties on studentsspecialties.FacultyId = studentsspecialtiefaculties.Id where Users.IsDeleted=false";

            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                students.Add(ReadStudent(reader));
            return students;
        }

        public bool Update(StudentDto value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Update Students Set UserId=@userId,AcceptanceDate=@acceptanceDate,SpecialtyId=@specialtyId,GroupId=@groupId Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@userId", value.UserId);
            command.Parameters.AddWithValue("@acceptanceDate", value.AcceptanceDate);
            command.Parameters.AddWithValue("@specialtyId", value.SpecialtyId);
            command.Parameters.AddWithValue("@groupId", value.GroupId);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
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
                        Id = reader.Get<int>("GroupSpecialtyId"),
                        Name = reader.Get<string>("GroupSpecialtyName"),
                        Faculty = new Faculty
                        {
                            Id = reader.Get<int>("GroupSpecialtyFacultyId"),
                            Name = reader.Get<string>("GroupSpecialtyFacultyName")
                        }
                    }
                },
                Specialty = new Specialty
                {
                    Id = reader.Get<int>("StudentSpecialtyId"),
                    Name = reader.Get<string>("StudentSpecialtyName"),
                    Faculty = new Faculty
                    {
                        Id = reader.Get<int>("StudentSpecialtyFacultyId"),
                        Name = reader.Get<string>("StudentSpecialtyFacultyName")
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
