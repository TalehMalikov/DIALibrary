using Library.Core.Domain.Dtos;
using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class AuthViewModel
    {
        public AccountLoginDto AccountLoginDto { get; set; }
        public string AccountName { get; set; }
        public Account Account { get; set; }
        public string Password { get; set; }
    }
}
