using Library.Entities.Concrete;
using Library.Entities.Dtos;

namespace Library.Admin.Models
{
    public class AuthorViewModel
    {
        public Author Author { get; set; }
        public List<Author> Authors { get; set; }

        public FileAuthorDto FileAuthor { get; set; }
        public FileAuthorDtoForCrud FileAuthorForCrud { get; set; }
    }
}
