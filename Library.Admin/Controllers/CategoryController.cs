using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new CategoryViewModel();
            var result = await _categoryService.GetAll();
            model.CategoryList = result.Data;
            return View(model);
        }

        public async Task<IActionResult> SaveCategory(int id)
        {

            if (id == 0)
                return PartialView();
            var model = new CategoryViewModel();
            var result = await _categoryService.Get(id);
            model.Category = result.Data;
            return PartialView(model);
        }
    }
}
