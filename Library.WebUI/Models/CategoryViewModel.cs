using Library.Core.Result.Concrete;
using Library.Entities.Concrete;

namespace Library.WebUI.Models
{
    public class CategoryViewModel
    {
        public DataResult<List<Category>> CategoryList { get; set; }
    }
}
