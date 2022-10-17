using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class StudentViewModel
    {
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
    }
}
