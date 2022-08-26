using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Library.WebUI.Systems;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

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

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultBookPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "img/*");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        [HttpGet]
        public IActionResult File(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultBookPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "pdf/*");
            }
            catch
            {
                return Ok();
            }
        }
    }
}
