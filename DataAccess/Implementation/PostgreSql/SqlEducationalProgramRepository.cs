using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlEducationalProgramRepository : IEducationalProgramRepository
    {
        private readonly string _connectionString;

        public SqlEducationalProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool Add(EducationalProgram value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Insert Into EducationalPrograms(Name,EducationLevel,FilePath,IsActive,LastModified,SpecialtyId,ProgramDate,EducationTime)" +
                " Values(@name,@educationLevel,@filePath,@isActive,@lastModified,@specialtyId,@programDate,@educationTime,@guid)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@educationLevel", value.EducationLevel);
            command.Parameters.AddWithValue("@filePath", value.FilePath);
            command.Parameters.AddWithValue("@isActive", value.IsActive);
            command.Parameters.AddWithValue("@lastModified", DateTime.Now);
            command.Parameters.AddWithValue("@specialtyId", value.Specialty.Id);
            command.Parameters.AddWithValue("@programDate", value.ProgramDate);
            command.Parameters.AddWithValue("@educationTime", value.EducationTime);
            command.Parameters.AddWithValue("@guid", value.GUID);
            return 1 == command.ExecuteNonQuery();
        }

        public EducationalProgram Get(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Select EducationalPrograms.Id as ProgramId, EducationalPrograms.Name as ProgramName,EducationalPrograms.EducationLevel,EducationalPrograms.FilePath,EducationalPrograms.IsActive," +
                "EducationalPrograms.LastModified,EducationalPrograms.ProgramDate,EducationalPrograms.EducationTime," +
                "Specialties.Id as SpecialtyId, Specialties.Name as SpecialtyName,guid," +
                "Faculties.Id as FacultyId,Faculties.Name as FacultyName from EducationalPrograms " +
                "inner join Specialties on EducationalPrograms.SpecialtyId=Specialties.Id " +
                "inner join Faculties on Specialties.FacultyId=Faculties.Id where EducationalPrograms.Id=@id and IsActive=true";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadEducationalProgram(reader);
            return null;
        }

        public List<EducationalProgram> GetAll()
        {
            List<EducationalProgram> list = new List<EducationalProgram>();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Select EducationalPrograms.Id as ProgramId, EducationalPrograms.Name as ProgramName,EducationalPrograms.EducationLevel,EducationalPrograms.FilePath,EducationalPrograms.IsActive," +
                "EducationalPrograms.LastModified,EducationalPrograms.ProgramDate,EducationalPrograms.EducationTime," +
                "Specialties.Id as SpecialtyId, Specialties.Name as SpecialtyName,guid," +
                "Faculties.Id as FacultyId,Faculties.Name as FacultyName from EducationalPrograms " +
                "inner join Specialties on EducationalPrograms.SpecialtyId=Specialties.Id " +
                "inner join Faculties on Specialties.FacultyId=Faculties.Id where IsActive=true";
            using NpgsqlCommand command = new(cmdString, connection);
            var reader = command.ExecuteReader();
            while(reader.Read())
                list.Add(ReadEducationalProgram(reader));
            return list;
        }

        public bool Update(EducationalProgram value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Update EducationalPrograms Set Name=@name,EducationLevel=@educationLevel,Guid=@guid,FilePath=@filePath," +
                "IsActive=@isActive,LastModified=@lastModified,SpecialtyId=@specialtyId,ProgramDate=@programDate," +
                "EducationDate=@educationDate Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", value.Id);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@educationLevel", value.EducationLevel);
            command.Parameters.AddWithValue("@filePath", value.FilePath);
            command.Parameters.AddWithValue("@isActive", value.IsActive);
            command.Parameters.AddWithValue("@lastModified", DateTime.Now);
            command.Parameters.AddWithValue("@specialtyId", value.Specialty.Id);
            command.Parameters.AddWithValue("@programDate", value.ProgramDate);
            command.Parameters.AddWithValue("@educationTime", value.EducationTime);
            command.Parameters.AddWithValue("@guid", value.GUID);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "update EducationalPrograms set IsActive=false Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public List<EducationalProgram> GetAllBySpecialtyId(int id)
        {
            List<EducationalProgram> list = new List<EducationalProgram>();
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString =
                "Select EducationalPrograms.Id as ProgramId, EducationalPrograms.Name as ProgramName,EducationalPrograms.EducationLevel,EducationalPrograms.FilePath,EducationalPrograms.IsActive," +
                "EducationalPrograms.LastModified,EducationalPrograms.ProgramDate,EducationalPrograms.EducationTime," +
                "Specialties.Id as SpecialtyId, Specialties.Name as SpecialtyName,guid," +
                "Faculties.Id as FacultyId,Faculties.Name as FacultyName from EducationalPrograms " +
                "inner join Specialties on EducationalPrograms.SpecialtyId=Specialties.Id " +
                "inner join Faculties on Specialties.FacultyId=Faculties.Id where Specialties.Id=@id and IsActive=true";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            while(reader.Read())
                list.Add(ReadEducationalProgram(reader));
            return list;
        }

        private EducationalProgram ReadEducationalProgram(NpgsqlDataReader reader)
        {
            return new EducationalProgram
            {
                Id = reader.Get<int>("ProgramId"),
                FilePath = reader.Get<string>("FilePath"),
                EducationLevel = reader.Get<string>("EducationLevel"),
                EducationTime = reader.Get<int>("EducationTime"),
                IsActive = reader.Get<bool>("IsActive"),
                LastModified = reader.Get<DateTime>("LastModified"),
                Name = reader.Get<string>("ProgramName"),
                ProgramDate = reader.Get<DateTime>("ProgramDate"),
                GUID = reader.Get<string>("Guid"),
                Specialty = new Specialty
                {
                    Id = reader.Get<int>("SpecialtyId"),
                    Name = reader.Get<string>("SpecialtyName"),
                    Faculty = new Faculty
                    {
                        Id = reader.Get<int>("FacultyId"),
                        Name = reader.Get<string>("FacultyName")
                    }
                }
            };
        }
    }
}
