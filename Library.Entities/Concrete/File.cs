using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class File : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Language OriginalLanguage { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public bool Status { get; set; }
        public FileType Type { get; set; }
    }
}
