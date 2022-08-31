using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class FileViewModel
    {
        public DataResult<List<File>> Files { get; set; }
        public DataResult<FileAuthorDto> FileAuthor { get; set; }
        public DataResult<List<FileAuthorDto>> FileAuthors { get; set; }
        public string Name { get; set; }
    }
}
