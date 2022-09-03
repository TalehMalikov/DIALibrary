using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Language OriginalLanguage { get; set; }
        public Language PublicationLanguage { get; set; }
        public bool EditionStatus { get; set; }
        public bool ExistingStatus { get; set; }
        public FileType FileType { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public string PublisherName { get; set; }
        public DateTime PublicationDate { get; set; }
        public int PageNumber { get; set; }
        public string Description { get; set; }
        public string GUID { get; set; }
        public DateTime LastModified { get; set; }
    }
}
