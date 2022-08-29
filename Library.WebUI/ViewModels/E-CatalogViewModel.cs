using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.ViewModels
{
    public class E_CatalogViewModel
    {
        public DataResult<List<Category>> CategoryList { get; set; }
        public DataResult<List<File>> FilteredFiles { get; set; }
        public DataResult<List<FileType>> FileTypes { get; set; }
    }
}
