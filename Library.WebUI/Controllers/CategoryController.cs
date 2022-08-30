using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
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
            CategoryFileViewModel categoryFileViewModel = new CategoryFileViewModel();
            categoryFileViewModel.CategoryModel.CategoryList = await _categoryService.GetAll();
            categoryFileViewModel.CategoryModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            return View(categoryFileViewModel);
        }
    }
}
