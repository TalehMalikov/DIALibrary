﻿using Library.Core.Extensions;
using Library.Core.Utils;
using Library.DataAccess.Abstraction;
using Library.Entities.Concrete;
using Npgsql;

namespace Library.DataAccess.Implementation.PostgreSql
{
    public class SqlAccountRepository : BaseRepository, IAccountRepository
    {
        public SqlAccountRepository(string connectionString) : base(connectionString)
        {
        }

        public bool Add(Account value)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string cmdText = "insert into Accounts(AccountName,UserId,PasswordHash,Email,LastModified) " +
                                 "values(@accountname,@userid,@passwordhash,@email,@lastmodified)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@accountname", value.AccountName);
                    cmd.Parameters.AddWithValue("@userid", value.User.Id);
                    cmd.Parameters.AddWithValue("@passwordhash", SecurityUtil.ComputeSha256Hash(value.PasswordHash));
                    cmd.Parameters.AddWithValue("@email", value.Email);
                    cmd.Parameters.AddWithValue("@lastmodified", value.LastModified);

                    int affectedCount = cmd.ExecuteNonQuery();
                    return affectedCount == 1;
                }
            }
        }

        public bool Delete(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string cmdText = "update Accounts set IsDeleted = true where Id = @id";
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    int affectedCount = cmd.ExecuteNonQuery();
                    return affectedCount == 1;
                }

            }
        }

        public Account Get(int id)
        {
            using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
            connection.Open();
            string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                           "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                           "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender," +
                           "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                           "from accounts inner join Users on Users.id = accounts.userid where Accounts.Id=@id and Users.IsDeleted=false and Accounts.IsDeleted = false";

            using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@id", id);

                var reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    var Employee = ReadAccount(reader);
                    return Employee;
                } 
                    
            }
            return null;
        }

        public List<Account> GetAll()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = "select Accounts.id as accountid,accountname,passwordhash,email," +
                               "Accounts.isdeleted as AccountIsDeleted,Accounts.lastmodified as AccountLastModified, " +
                               "Users.Id as UserId,firstname,lastname,fathername,birthdate,gender," +
                               "Users.IsDeleted as UserIsDeleted,Users.LastModified as UserLastModified " +
                               "from accounts inner join Users on Users.id = accounts.userid where Users.IsDeleted=false and Accounts.IsDeleted = false";
                
                List<Account> accounts = new List<Account>();

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        var account = ReadAccount(reader);
                        accounts.Add(account);
                    }
                    return accounts;
                }
            }
        }

        public bool Update(Account value)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string cmdText = "update Accounts set AccountName=@accountname, UserId=@userid, " +
                                 "PasswordHash=@passwordhash, Email=@email, LastModified=@lastmodified " +
                                 "where Id=@id and IsDeleted=false";
                using (NpgsqlCommand cmd = new NpgsqlCommand(cmdText, connection))
                {
                    cmd.Parameters.AddWithValue("@id", value.Id);
                    cmd.Parameters.AddWithValue("@accountname", value.AccountName);
                    cmd.Parameters.AddWithValue("@userid", value.User.Id);
                    cmd.Parameters.AddWithValue("@passwordhash", value.PasswordHash);
                    cmd.Parameters.AddWithValue("@email", value.Email);
                    cmd.Parameters.AddWithValue("@lastmodified", value.LastModified);

                    int affectedCount = cmd.ExecuteNonQuery();
                    return affectedCount == 1;
                }
            }
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
                    LastModified = reader.Get<DateTime>("UserLastModified")
                },
                AccountName = reader.Get<string>("AccountName"),
                PasswordHash = reader.Get<string>("PasswordHash"),
                Email = reader.Get<string>("Email"),
                IsDeleted = reader.Get<bool>("AccountIsDeleted"),
                LastModified = reader.Get<DateTime>("AccountLastModified")
            };
        }
    }
}