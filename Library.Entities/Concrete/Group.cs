using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Group : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Sector Sector { get; set; }
        public Specialty Speciality { get; set; }
    }
}
