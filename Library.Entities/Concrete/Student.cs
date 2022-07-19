using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Student : IEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public int SpecialtyId { get; set; }
        public int GroupId { get; set; }
    }
}
