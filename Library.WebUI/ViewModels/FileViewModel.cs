using Library.Core.Result.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class FileViewModel
    {
        public DataResult<List<File>> Files { get; set; }
        public DataResult<File> File { get; set; }
    }
}
