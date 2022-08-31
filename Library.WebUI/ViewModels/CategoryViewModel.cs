using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Models
{
    public class CategoryViewModel
    {
        public DataResult<List<Category>> CategoryList { get; set; }
        public  DataResult<Category> Category { get; set; }

        public DataResult<List<File>> NewAddedBookList { get; set; }
    }
}
