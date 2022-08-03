using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlSectorRepository : BaseRepository, ISectorRepository
    {
        public bool Add(Sector value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Insert Into Sectors(Name) Values(@name)";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@name", value.Name);
            return 1 == cmd.ExecuteNonQuery();

        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Delete From Sectors Where Id = @id";
            NpgsqlCommand cmd = new NpgsqlCommand(cmdString, connection);
            cmd.Parameters.AddWithValue("@id", id);
            return 1 == cmd.ExecuteNonQuery();
        }

        public Sector Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Sectors Where Id=@id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            if (reader.Read())
                return ReadSector(reader);
            return null;
        }

        public List<Sector> GetAll()
        {
            List<Sector> sectors = new List<Sector>();
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Select * From Sectors";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            var reader = command.ExecuteReader();
            while (reader.Read()) 
                sectors.Add(ReadSector(reader));
            return sectors;
        }

        public bool Update(Sector value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string cmdString = "Update Sectors Set Name = @name where Id = @id";
            using NpgsqlCommand command = new NpgsqlCommand(cmdString, connection);
            command.Parameters.AddWithValue("@name", value.Name);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1==command.ExecuteNonQuery();
        }

        private Sector ReadSector(NpgsqlDataReader reader)
        {
            return new Sector
            {
                Id = reader.Get<int>(nameof(Sector.Id)),
                Name = reader.Get<string>(nameof(Sector.Name))
            };
        }
    }
}
