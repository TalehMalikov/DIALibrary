using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public User User { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public Specialty Specialty { get; set; }
        public Group Group { get; set; }
    }
}
