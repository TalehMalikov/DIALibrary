using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class EducationalProgram : BaseEntity
    {
        public string Name { get; set; }
        public string EducationLevel { get; set; }
        public string FilePath { get; set; }
        public string QrCodePhotoPath { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastModified { get; set; }
        public Specialty Specialty { get; set; }
        public DateTime ProgramDate { get; set; }
        public int EducationTime { get; set; }
        public string GUID { get; set; }

    }
}
