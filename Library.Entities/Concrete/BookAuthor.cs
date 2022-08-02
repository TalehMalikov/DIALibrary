using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class BookAuthor : BaseEntity
    {
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
