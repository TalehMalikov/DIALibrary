using Library.Entities.Concrete;

namespace Library.Admin.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public List<Category> CategoryList { get; set; }
    }
}
