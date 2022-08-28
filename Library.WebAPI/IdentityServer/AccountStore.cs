using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Library.WebAPI.IdentityServer
{
    public class AccountStore : IUserStore<Account>,
                             IUserPasswordStore<Account>,
                             IUserRoleStore<Account>
    {
        private readonly IAccountService _db;
        public AccountStore(IAccountService accountdb)
        {
            this._db = accountdb;
        }

        #region IUserStore implementation

        public Task<IdentityResult> CreateAsync(Account user, CancellationToken cancellationToken)
        {
            return null; 
        }

        public Task<IdentityResult> DeleteAsync(Account user, CancellationToken cancellationToken)
        {
            return null;
        }

        public void Dispose()
        {
        }

        public Task<Account> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var user = _db.Get(int.Parse(userId)).Data;
            return Task.FromResult(user);

        }

        public Task<Account> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var user = _db.GetByEmail(normalizedUserName).Data;

            return Task.FromResult(user);
        }

        public Task<string> GetNormalizedUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<string> GetUserIdAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task SetNormalizedUserNameAsync(Account user, string normalizedName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();
            
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (normalizedName == null)
            {
                throw new ArgumentNullException(nameof(normalizedName));
            }

            user.Email = normalizedName;

            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Account user, string userName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Account user, CancellationToken cancellationToken)
        {
            return null;
        }

        #endregion

        #region IUserPasswordStore implementation

        public Task<string> GetPasswordHashAsync(Account user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(Account user, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task SetPasswordHashAsync(Account user, string passwordHash, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region IUserRoleStore implementation

        public Task AddToRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task AddToRolesAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task<IList<string>> GetRolesAsync(Account user, CancellationToken cancellationToken)
        {
            IList<string> roles = new List<string>();
            foreach (var role in _db.GetRoles(user).Data)
            {
                roles.Add(role.Name);
            }
            return Task.FromResult(roles);
        }

        public Task<IList<Account>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            return null;
        }

        public Task<bool> IsInRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
             var x = _db.GetRoles(user).Data.FirstOrDefault(x => x.Name == roleName);
             return x!=null ? Task.FromResult(true) : Task.FromResult(false);
        }

        public Task RemoveFromRoleAsync(Account user, string roleName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #endregion
    }
}
