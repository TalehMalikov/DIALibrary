﻿using Library.WebUI.ViewModels;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Library.Core.DefaultSystemPath;

namespace Library.WebUI.Controllers
{
    public class OurLibraryController : Controller
    {
        private readonly IExternalSourceService _externalSourceService;
        private readonly IFileTypeService _fileTypeService;

        public OurLibraryController(IExternalSourceService externalSourceService,IFileTypeService fileTypeService)
        {
            _externalSourceService = externalSourceService;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> MoreInfo()
        {
            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

            return View();
        }

        public async Task<IActionResult>ExternalSource()
        {
            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

            ExternalSourceViewModel viewModel = new ExternalSourceViewModel();
            viewModel.ExternalSourceList = await _externalSourceService.GetAll();

            return View(viewModel);
        }

        public IActionResult GetPhoto(string path)
        {
            try
            {
                string fullpath = Path.Combine(DefaultPath.OriginalDefaultExternalSourcePath, path);

                var content = System.IO.File.ReadAllBytes(fullpath);

                return File(content, "img/*");
            }
            catch
            {
                return Ok();
            }
        }
    }
}
