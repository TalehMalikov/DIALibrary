using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class UserViewModel
    {
        public User User { get; set; }
        public Student Student { get; set; }
        public List<User> Users { get; set; } = new List<User>();
        public SelectList SpecialtyList { get; set; }
        public SelectList GroupList { get; set; }
    }
}
