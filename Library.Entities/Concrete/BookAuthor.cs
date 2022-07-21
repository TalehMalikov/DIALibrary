using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class BookAuthor : IEntity
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Author Author { get; set; }
    }
}
