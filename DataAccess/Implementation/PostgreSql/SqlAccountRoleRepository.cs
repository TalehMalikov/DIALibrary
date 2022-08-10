using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAccountRoleRepository : IAccountRoleRepository
    {
        private readonly string _connectionString;
        public SqlAccountRoleRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Add(AccountRole value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Insert Into AccountRoles(AccountId,RoleId) Values(@accountId,@roleId)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@accountId", value.Account.Id);
            command.Parameters.AddWithValue("@roleId", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Delete From AccountRoles Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public AccountRole Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select accountroles.id as accountroleid ,accountid,userid,firstname," +
                            "lastname,fathername,birthdate,gender,users.isdeleted as userisdeleted," +
                            "users.lastmodified as userlastmodified,accountname,passwordhash,email," +
                            "accounts.isdeleted as accountisdeleted,accounts.lastmodified as accountlastmodified," +
                            "roleid, roles.name as rolename from accountroles join accounts on accountid = accounts.id " +
                            "join users on userid = users.id join roles on roleid = roles.id " +
                            "where accountroles.id = @id and accounts.isdeleted=false and users.isdeleted=false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadAccountRoles(reader);
            return null;
        }

        public List<AccountRole> GetAll()
        {

            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select accountroles.id as accountroleid ,accountid,userid,firstname," +
                            "lastname,fathername,birthdate,gender,users.isdeleted as userisdeleted," +
                            "users.lastmodified as userlastmodified,accountname,passwordhash,email," +
                            "accounts.isdeleted as accountisdeleted,accounts.lastmodified as accountlastmodified," +
                            "roleid, roles.name as rolename from accountroles join accounts on accountid = accounts.id " +
                            "join users on userid = users.id join roles on roleid = roles.id " +
                            "where accounts.isdeleted=false and users.isdeleted=false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            var accountRoles = new List<AccountRole>();
            while (reader.Read())
                accountRoles.Add(ReadAccountRoles(reader));
            return accountRoles;
        }

        public bool Update(AccountRole value)
        {
            using NpgsqlConnection connection = new(_connectionString);
            connection.Open();
            string cmdString = "Update AccountRoles Set AccountId=@accountId, RoleId=@roleId Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@accountId", value.Account.Id);
            command.Parameters.AddWithValue("@roleId", value.Id);
            command.Parameters.AddWithValue("@id", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        private AccountRole ReadAccountRoles(NpgsqlDataReader reader)
        {
            return new AccountRole
            {
                Id = reader.Get<int>("AccountRoleId"),
                Account = new Account
                {
                    Id = reader.Get<int>("AccountId"),
                    AccountName = reader.Get<string>("AccountName"),
                    Email = reader.Get<string>("Email"),
                    IsDeleted = reader.Get<bool>("AccountIsDeleted"),
                    LastModified = reader.Get<DateTime>("AccountLastModified"),
                    PasswordHash = reader.Get<string>("Passwordhash"),
                    User = new User
                    {
                        Id = reader.Get<int>("UserId"),
                        IsDeleted = reader.Get<bool>("UserIsDeleted"),
                        LastModified = reader.Get<DateTime>("UserLastModified"),
                        BirthDate = reader.Get<DateTime>("BirthDate"),
                        FatherName = reader.Get<string>("FatherName"),
                        FirstName = reader.Get<string>("FirstName"),
                        Gender = reader.Get<bool>("Gender"),
                        LastName = reader.Get<string>("LastName")
                    }
                },
                Role = new Role
                {
                    Id = reader.Get<int>("RoleId"),
                    Name = reader.Get<string>("RoleName")
                }
            };
        }
    }
}
