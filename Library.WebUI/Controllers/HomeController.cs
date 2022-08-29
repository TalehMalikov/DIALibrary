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
            ViewData["E-Kitabxana"] = _stringLocalizer["E-Kitabxana"].Value;
            ViewData["Giriş"] = _stringLocalizer["Giriş"].Value;
            ViewData["E-Kitablar"] = _stringLocalizer["E-Kitablar"].Value;
            ViewData["Yeniliklər"] = _stringLocalizer["Yeniliklər"].Value;
            ViewData["Ümumi məlumat"] = _stringLocalizer["Ümumi məlumat"].Value;
            ViewData["Digər kitabxanalar"] = _stringLocalizer["Digər kitabxanalar"].Value;
            ViewData["Ana səhifə"] = _stringLocalizer["Ana səhifə"].Value;
            ViewData["Bizim kitabxana"] = _stringLocalizer["Bizim kitabxana"].Value;
            ViewData["E-Resurslar"] = _stringLocalizer["E-Resurslar"].Value;
            ViewData["E-Kataloq"] = _stringLocalizer["E-Kataloq"].Value;
            ViewData["Giriş"] = _stringLocalizer["Giriş"].Value;
            #endregion

            AuthenticationViewModel authViewModel = new AuthenticationViewModel
            {
                NewAddedBookList = await _categoryService.GetNewAddedBooks()
            };
            return View(authViewModel);
        }
    }
}
