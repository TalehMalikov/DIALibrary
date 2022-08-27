namespace Library.WebUI.Models
{
    public class CategoryFileViewModel
    {
        public CategoryViewModel CategoryModel { get; set; } = new CategoryViewModel();
        public FileViewModel FileModel { get; set; } = new FileViewModel();
    }
}
