using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Student : BaseEntity
    {
        public int UserId { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public int SpecialtyId { get; set; }
        public int GroupId { get; set; }

        public User User { get; set; }
        public Specialty Specialty { get; set; }
        public Group Group { get; set; }
    }
}
