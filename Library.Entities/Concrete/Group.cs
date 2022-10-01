using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public Sector Sector { get; set; } 
        public Specialty Speciality { get; set; }
    }
}
