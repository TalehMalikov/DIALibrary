using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;

        public CategoryController(ICategoryService categoryService, IFileService fileService)
        {
            _categoryService = categoryService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CategoryFileViewModel categoryFileViewModel = new CategoryFileViewModel
            {
                CategoryModel = new CategoryViewModel
                {
                    CategoryList = await _categoryService.GetAll(),
                    NewAddedBookList = await _categoryService.GetNewAddedBooks()
                }
            };
            return View(categoryFileViewModel);
        }
    }
}
