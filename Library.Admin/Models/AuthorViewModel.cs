using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public List<Author> Authors { get; set; }
    }
}
