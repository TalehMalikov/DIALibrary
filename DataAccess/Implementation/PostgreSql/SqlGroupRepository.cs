using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlGroupRepository : BaseRepository, IGroupRepository
    {
        public bool Add(Group value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Groups(Name,SpecialtyId,SectorId) Values(@name,@speacialtyId,@sectorId)";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@specialtyId", value.Speciality.Id);
            command.Parameters.AddWithValue("@sectorId", value.Sector.Id);
            return 1 == command.ExecuteNonQuery();

        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Delete From Groups Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public Group Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString =
                "select Groups.Id as GroupId, Groups.Name as GroupName,Sectors.Id as SectorId, Sectors.Name as SectorName," +
                " Specialties.Id as SpecialtyId,Specialties.Name as SpecialtyName,Faculties.Id as FacultyId,Faculties.Name as FacultyName from Groups" +
                " inner join Sectors on Groups.SectorId=Sectors.Id" +
                " inner join Specialties on Groups.SpecialtyId=Specialties.Id" +
                " inner join Faculties on Specialties.FacultyId=Faculties.Id Where Groups.Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadGroup(reader);
            return null;
        }

        public List<Group> GetAll()
        {
            List<Group> groupList = new List<Group>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString =
                "select Groups.Id as GroupId, Groups.Name as GroupName,Sectors.Id as SectorId, Sectors.Name as SectorName," +
                " Specialties.Id as SpecialtyId,Specialties.Name as SpecialtyName,Faculties.Id as FacultyId,Faculties.Name as FacultyName from Groups" +
                " inner join Sectors on Groups.SectorId=Sectors.Id" +
                " inner join Specialties on Groups.SpecialtyId=Specialties.Id" +
                " inner join Faculties on Specialties.FacultyId=Faculties.Id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
                groupList.Add(ReadGroup(reader));
            return groupList;

        }

        public bool Update(Group value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Groups Set Name=@name,SpecialtyId=@specialtyId,SectorId=@sectorId Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@specialtyId", value.Speciality.Id);
            command.Parameters.AddWithValue("@sectorId",value.Sector.Id);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private Group ReadGroup(NpgsqlDataReader reader)
        {
            return new Group
            {
                Id = reader.Get<int>("GroupId"),
                Name = reader.Get<string>("GroupName"),
                Sector = new Sector
                {
                    Id = reader.Get<int>("SectorId"),
                    Name = reader.Get<string>("SectorName")
                },
                Speciality = new Speciality
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
