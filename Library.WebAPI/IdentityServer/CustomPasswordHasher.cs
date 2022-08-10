using Library.Core.Utils;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Identity;

namespace Library.WebAPI.IdentityServer
{
    public class CustomPasswordHasher : IPasswordHasher<Account>
    {
        public string HashPassword(Account user, string password)
        {
            throw new NotImplementedException();
        }

        public PasswordVerificationResult VerifyHashedPassword(Account user, string hashedPassword, string providedPassword)
        {
            var providedPasswordHash = SecurityUtil.ComputeSha256Hash(providedPassword);

            if (hashedPassword == providedPasswordHash)
                return PasswordVerificationResult.Success;

            return PasswordVerificationResult.Failed;
        }
    }
}
