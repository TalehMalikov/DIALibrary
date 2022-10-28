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

        [HttpGet]
        public async Task<IActionResult> SaveCategory(int id)
        {

            if (id == 0)
                return PartialView();
            var model = new CategoryViewModel();
            var result = await _categoryService.Get(id);
            model.Category = result.Data;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveCategory(CategoryViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Category.Id == 0)
            {
                await _categoryService.Add(token, model.Category);
                return RedirectToAction("Index");
            }
            await _categoryService.Update(token, model.Category);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            await _categoryService.Delete(token, id);
            return RedirectToAction("Index");
        }
    }
}
