using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class GroupDto : BaseDto
    {
        public string Name { get; set; }
        public int SectorId { get; set; }
        public int SpecialityId { get; set; }
    }
}
