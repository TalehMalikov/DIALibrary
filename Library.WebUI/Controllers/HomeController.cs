using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IStringLocalizer<HomeController> _stringLocalizer;

        public HomeController(ICategoryService categoryService, IStringLocalizer<HomeController> stringLocalizer)
        {
            _categoryService = categoryService;
            _stringLocalizer = stringLocalizer;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            #region ViewData

            ViewData["Azərbaycan Respublikası Prezidenti yanında Dövlət İdarəçilik Akademiyası Elektron Kitabxanası"] =
                _stringLocalizer[
                        "Azərbaycan Respublikası Prezidenti yanında Dövlət İdarəçilik Akademiyası Elektron Kitabxanası"]
                    .Value;
            #endregion

            AuthenticationViewModel authViewModel = new AuthenticationViewModel
            {
                NewAddedBookList = await _categoryService.GetNewAddedBooks()
            };
            return View(authViewModel);
        }
    }
}
