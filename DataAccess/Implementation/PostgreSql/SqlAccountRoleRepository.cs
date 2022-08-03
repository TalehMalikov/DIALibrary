using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{

    public class SqlAccountRoleRepository : BaseRepository, IAccountRoleRepository
    {
        public SqlAccountRoleRepository(string connectionString) : base(connectionString)
        {
        }
        public bool Add(AccountRole value)
        {

            using NpgsqlConnection connection = new(connectionString);
            connection.Open();
            string cmdString = "Insert Into AccountRoles(AccountId,RoleId) Values(@accountId,@roleId)";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@accountId", value.Account.Id);
            command.Parameters.AddWithValue("@roleId", value.Id);
            return 1 == command.ExecuteNonQuery();
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new(connectionString);
            connection.Open();
            string cmdString = "Delete From AccountRoles Where Id=@id";
            using NpgsqlCommand command = new(cmdString, connection);
            command.Parameters.AddWithValue("@id", id);
            return 1 == command.ExecuteNonQuery();
        }

        public AccountRole Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccountRole> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Update(AccountRole value)
        {
            using NpgsqlConnection connection = new(connectionString);
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
                    PasswordHash = reader.Get<string>("Password"),
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
