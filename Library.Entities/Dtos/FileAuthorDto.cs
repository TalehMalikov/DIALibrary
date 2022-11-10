using Library.Entities.Abstract;
using Library.Entities.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.Entities.Dtos
{
    public class FileAuthorDto  : BaseDto
    {
        public File File { get; set; }
        public List<Author> Authors { get; set; }

        public int FileId { get; set; }
        public List<int> AuthorIds { get; set; } 
    }
}
