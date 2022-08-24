using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Publication : BaseEntity
    {
        public File Book { get; set; }
        public string Photo { get; set; }
        public string File { get; set; }
        public string PublisherName { get; set; }
        public short PageNumber { get; set; }
        public Language PublicationLanguage { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
