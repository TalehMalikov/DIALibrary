using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
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
        private readonly IFileTypeService _fileTypeService;
        private readonly IFileService _fileService;

        public HomeController(ICategoryService categoryService,IFileTypeService fileTypeService, IStringLocalizer<HomeController> stringLocalizer,
                              IFileService fileService)
        {
            _categoryService = categoryService;
            _stringLocalizer = stringLocalizer;
            _fileTypeService = fileTypeService;
            _fileService = fileService;
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
            ViewData["Şəxsi kabinet"] = _stringLocalizer["Şəxsi kabinet"].Value;
            #endregion

            var files = await _categoryService.GetNewAddedBooks();

            AuthenticationViewModel authViewModel = new AuthenticationViewModel();
            var fileAuthors = new List<FileAuthorDto>();
            var fileAuthor = await _fileService.GetFileWithAuthors(files.Data[0].Id);
            foreach (var item in files.Data)
            {
                fileAuthor = await _fileService.GetFileWithAuthors(item.Id);
                fileAuthors.Add(fileAuthor.Data);
            }
            authViewModel.NewAddedFileAuthorList = fileAuthors;

            var filetypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = filetypes.Data;

            return View(authViewModel);
        }
    }
}
