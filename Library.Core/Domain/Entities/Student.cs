namespace Library.Core.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime AcceptanceDate { get; set; }
        public int SpecialtyId { get; set; }
        public int GroupId { get; set; }
    }
}
