using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using File = Library.Entities.Concrete.File;
namespace Library.Admin.Models
{
    public class ResourceViewModel
    {
        public List<File> Files { get; set; }

        public IFormFile AddedPicture { get; set; }

        public IFormFile AddedFile { get; set; }

        public SelectList CategoryList { get; set; }
        public SelectList LanguageList { get; set; }
        public SelectList EditionList { get; set; }
        public int CategoryId { get; set; }
        public int OriginalLanguageId { get; set; }
        public int PublicationLanguageId { get; set; }
        public int EditionStatusId { get; set; }
        public File DeletedBook { get; set; }
        public File File { get; set; }
    }
}
