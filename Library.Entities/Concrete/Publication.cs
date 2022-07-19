using Library.Core.Domain.Abstract;

namespace Library.Entities.Concrete
{
    public class Publication : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string PhotoPath { get; set; }
        public string FilePath { get; set; }
        public string PublisherName { get; set; }
        public short PageNumber { get; set; }
        public int PublicationLanguageId { get; set; }
        public DateTime PublicationDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
