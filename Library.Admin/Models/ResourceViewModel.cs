using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using File = Library.Entities.Concrete.File;
namespace Library.Admin.Models
{
    public class ResourceViewModel
    {
        public List<File> Files { get; set; }
        public IFormFile AddedFile { get; set; }
        public IFormFile AddedPicture { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList LanguageList { get; set; }
        public SelectList EditionList { get; set; }
        public int CategoryId, OriginalLanguageId, PublicationLanguageId,EditionStatusId;
        public File DeletedBook { get; set; }
        public File File { get; set; }
    }
}
