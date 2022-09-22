using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
    }
}
