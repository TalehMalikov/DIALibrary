using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthenticationViewModel authViewModel = new AuthenticationViewModel();
            authViewModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            return View(authViewModel);
        }
    }
}
