using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Publication : BaseEntity
    {
        public Book Book { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public string PublisherName { get; set; }
        public short PageNumber { get; set; }
        public Language PublicationLanguage { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
