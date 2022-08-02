using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Book : BaseEntity
    {
        public string Name { get; set; }
        public Category Category { get; set; }
        public Language OriginaLanguage { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
