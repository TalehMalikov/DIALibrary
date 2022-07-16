namespace Library.Core.Domain.Entities
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SectorId { get; set; }
        public int SpecialtyId { get; set; }
    }
}
