using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class BookAuthor : IEntity
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int AuthorId { get; set; }
    }
}
