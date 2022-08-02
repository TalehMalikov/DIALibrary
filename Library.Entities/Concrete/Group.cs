using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }
        public int SectorId { get; set; }
        public int SpecialtyId { get; set; }

        public Sector Sector { get; set; } 
        public Speciality Speciality { get; set; }
    }
}
