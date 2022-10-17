using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class SpecialtyDto : BaseDto
    {
        public string Name { get; set; }
        public int FacultyId { get; set; }
    }
}
