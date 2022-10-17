using Library.Entities.Abstract;
using Library.Entities.Concrete;

namespace Library.Entities.Dtos
{
    public class FileAuthorDtoForCrud : BaseDto
    {
        public int FileId { get; set; }
        public int AuthorId { get; set; }
    }
}
