using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
