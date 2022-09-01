using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.ViewModels
{
    public class ECatalogViewModel
    {
        public SelectList CategoryList { get; set; }
        public SelectList FileTypeList { get; set; }
        //public List<File> FilteredFileList { get; set; }
        public List<FileAuthorDto> FilteredFileAuthors { get; set; }

        public string AuthorFilter { get; set; }
        public string BookFilter { get; set; }
        public int PublicationYearMax { get; set; }
        public int PublicationYearMin { get; set; }

        public int SelectedCategoryId { get; set; }
        public int SelectedFileTypeId { get; set; }

    }
}
