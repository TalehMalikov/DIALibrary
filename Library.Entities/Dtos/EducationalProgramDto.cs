using Library.Entities.Abstract;
using Library.Entities.Concrete;

namespace Library.Entities.Dtos
{
    public class EducationalProgramDto : BaseDto
    {
        public string Name { get; set; }
        public string EducationLevel { get; set; }
        public string FilePath { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModified { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime ProgramDate { get; set; }
        public int EducationTime { get; set; }
        public string GUID { get; set; }
    }
}
