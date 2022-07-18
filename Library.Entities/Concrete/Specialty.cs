using Library.Core.Domain.Abstract;

namespace Library.Entities.Concrete
{
    public class Specialty : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FacultyId { get; set; }
    }
}
