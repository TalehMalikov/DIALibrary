using Library.Entities.Abstract;

namespace Library.Entities.Concrete
{
    public class BookAuthor : BaseEntity
    {

        public File File { get; set; }
        public Author Author { get; set; }
    }
}
