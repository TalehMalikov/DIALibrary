using Library.Core.Extensions;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAccountRepository : IAccountRepository
    {
        private readonly string _connectionString;
        public SqlAccountRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public int Add(AccountDto value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdText = "insert into Accounts(AccountName,UserId,PasswordHash,Email,LastModified) " +
                             "values(@accountname,@userId,@passwordHash,@email,@lastModified) RETURNING Accounts.Id";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@accountName", value.AccountName);
            cmd.Parameters.AddWithValue("@userid", value.UserId);
            cmd.Parameters.AddWithValue("@passwordHash", value.PasswordHash);
            cmd.Parameters.AddWithValue("@email", value.Email);
            cmd.Parameters.AddWithValue("@lastModified", value.LastModified);

            int affectedCount = Convert.ToInt32(cmd.ExecuteScalar());
            return affectedCount;
        }

        public bool Delete(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdText = "update Accounts set IsDeleted = true where Id = @id";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", id);
            int affectedCount = cmd.ExecuteNonQuery();
            return affectedCount == 1;
        }

        public Account Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select Accounts.id as accountId,accountName,passwordHash,Email," +
                           "Accounts.isDeleted as AccountIsDeleted,Accounts.LastModified as AccountLastModified, " +
                           "Users.Id as UserId,FirstName,LastName,FatherName,birthdate,gender,isStudent," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Accounts.Id=@id and Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@id", id);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadAccount(reader);
            return null;
        }

        public List<Account> GetAll()
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                           "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                           "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender,isStudent," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Users.IsDeleted=false and Accounts.IsDeleted = false";

            List<Account> accounts = new List<Account>();
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                accounts.Add(ReadAccount(reader));
            return accounts;
        }

        public Account GetByEmail(string email)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                           "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                           "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender,isStudent," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Accounts.Email=@email and Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@email", email);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadAccount(reader);
            return null;
        }

        public Account GetByAccountName(string accountName)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                           "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                           "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender,isStudent," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Accounts.AccountName=@accountName and Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadAccount(reader);
            return null;
        }

        public Account Login(string accountName)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                           "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                           "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender,isStudent," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Upper(Accounts.AccountName)=@accountName and Users.IsDeleted=false and Accounts.IsDeleted = false";
            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@accountName", accountName);
            var reader = cmd.ExecuteReader();
            if (reader.Read())
                return ReadAccount(reader);
            return null;
        }

        public List<Role> GetRoles(Account account)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string query = "select roleid,roles.name as rolename from accountroles " +
                           "join accounts on accountid = accounts.id " +
                           "join roles on roleid = roles.id " +
                           "where accountid = @accountid and accounts.isdeleted = false ";

            using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.Parameters.AddWithValue("@accountid", account.Id);
            var roles = new List<Role>();
            var reader = cmd.ExecuteReader();
            while (reader.Read())
                roles.Add(ReadRole(reader));
            return roles;
        }

        public bool Update(AccountDto value)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(_connectionString);
            connection.Open();
            string cmdText = "update Accounts set AccountName=@accountname, UserId=@userid, " +
                             "PasswordHash=@passwordhash, Email=@email, LastModified=@lastmodified, " +
                             "IsDeleted=@isDeleted where Id=@id";
            using NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection);
            cmd.Parameters.AddWithValue("@id", value.Id);
            cmd.Parameters.AddWithValue("@accountname", value.AccountName);
            cmd.Parameters.AddWithValue("@userid", value.UserId);
            cmd.Parameters.AddWithValue("@passwordhash", value.PasswordHash);
            cmd.Parameters.AddWithValue("@email", value.Email);
            cmd.Parameters.AddWithValue("@lastmodified", DateTime.Now);
            cmd.Parameters.AddWithValue("@isDeleted", value.IsDeleted);

            return 1 == cmd.ExecuteNonQuery();
        }
        private Account ReadAccount(NpgsqlDataReader reader)
        {
            return new Account
            {
                Id = reader.Get<int>("AccountId"),
                User = new User()
                {
                    Id = reader.Get<int>("UserId"),
                    FirstName = reader.Get<string>("FirstName"),
                    LastName = reader.Get<string>("LastName"),
                    FatherName = reader.Get<string>("FatherName"),
                    BirthDate = reader.Get<DateTime>("BirthDate"),
                    Gender = reader.Get<bool>("Gender"),
                    IsDeleted = reader.Get<bool>("UserIsDeleted"),
                    IsStudent = reader.Get<bool>("IsStudent"),
                    LastModified = reader.Get<DateTime>("UserLastModified")
                },
                AccountName = reader.Get<string>("AccountName"),
                PasswordHash = reader.Get<string>("PasswordHash"),
                Email = reader.Get<string>("Email"),
                IsDeleted = reader.Get<bool>("AccountIsDeleted"),
                LastModified = reader.Get<DateTime>("AccountLastModified")
            };
        }

        private Role ReadRole(NpgsqlDataReader reader)
        {
            return new Role()
            {
                Id = reader.Get<int>("roleid"),
                Name = reader.Get<string>("rolename")
            };
        }
    }
}
