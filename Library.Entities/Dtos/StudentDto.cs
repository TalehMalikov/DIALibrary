using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class StudentDto : BaseDto
    {
        public int UserId { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public int SpecialtyId { get; set; }
        public int GroupId { get; set; }
    }
}
