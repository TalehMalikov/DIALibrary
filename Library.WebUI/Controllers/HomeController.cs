using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;

        public HomeController(ICategoryService categoryService,IFileTypeService fileTypeService)
        {
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthenticationViewModel authViewModel = new AuthenticationViewModel();
            authViewModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            var filetypes = await _fileTypeService.GetAllFileTypes();
            authViewModel.AllFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = filetypes.Data;

            return View(authViewModel);
        }
    }
}
