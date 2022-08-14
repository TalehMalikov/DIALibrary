using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class Activity : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PhotoPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
