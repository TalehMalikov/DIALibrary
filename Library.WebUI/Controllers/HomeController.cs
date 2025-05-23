﻿using Library.Core.Result.Concrete;
using Library.Entities.Dtos;
using Library.WebUI.ViewModels;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace Library.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IFileService _fileService;

        public HomeController(ICategoryService categoryService,IFileTypeService fileTypeService,
                              IFileService fileService)
        {
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            

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

        public IActionResult NotFound()
        {
            return View();
        }
    }
}
