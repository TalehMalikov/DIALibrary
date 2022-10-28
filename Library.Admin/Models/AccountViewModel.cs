using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Models
{
    public class AccountViewModel
    {
        public AccountDto AccountDto { get; set; }
        public Account Account { get; set; }
        public List<Account> Accounts { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
