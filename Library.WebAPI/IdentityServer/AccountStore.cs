using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Library.WebAPI.IdentityServer
{
    public class AccountStore : IUserStore<Account>,
                             IUserPasswordStore<Account>,
                             IUserRoleStore<Account>
    {
        private readonly IAccountService db;
        public AccountStore(IAccountService accountdb)
        {
            this.db = accountdb;
        }

        #region IUserStore implementation

        public Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = db.Get(int.Parse(userId)).Data;
            return Task.FromResult(user);

        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = db.GetByEmail(normalizedUserName).Data;

            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserPasswordStore implementation

        public Task<string> GetPasswordHashAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task SetPasswordHashAsync(Account user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IUserRoleStore implementation

        public Task AddToRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(Account user, CancellationToken cancellationToken)
        {
            // get  UserRoles from db
           /* IList<string> roles = new List<string>
            {
                "A", "SA"
            };*/

            IList<string> roles = new List<string>();
            foreach (var role in db.GetRoles(user).Data)
            {
                roles.Add(role.Name);
            }
            return Task.FromResult(roles);
        }

        public Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
