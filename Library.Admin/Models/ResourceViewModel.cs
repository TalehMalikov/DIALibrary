using File = Library.Entities.Concrete.File;
namespace Library.Admin.Models
{
    public class ResourceViewModel
    {
        public List<File> Files { get; set; }
        public File DeletedBook { get; set; }
        public File File { get; set; }
    }
}
