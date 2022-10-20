using Library.Entities.Abstract;

namespace Library.Entities.Dtos
{
    public class FileAuthorDtoForCrud : BaseDto
    {
        public int FileId { get; set; }
        public int AuthorId { get; set; }
    }
}
