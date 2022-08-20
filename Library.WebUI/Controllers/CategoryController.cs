using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAll();
            var model = new CategoryViewModel
            {
                CategoryList = result
            };
            return View(model);
        }

    }
}
