using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class AccountViewModel
    {
        public Account Account { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
    }
}
