﻿using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Library.WebUI.Systems;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IFileService _fileService;

        public CategoryController(ICategoryService categoryService, IFileService fileService)
        {
            _categoryService = categoryService;
            _fileService = fileService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(CategoryViewModel model)
        {
            var result = await _categoryService.GetAll();
            model.CategoryList = result;
            model.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            return View(model);
        }

        [HttpGet]
        public IActionResult Picture(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultBookPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "img/*");
            }
            catch
            {
                return BadRequest("Not found");
            }
        }

        [HttpGet]
        public IActionResult File(string name)
        {
            try
            {
                string fullPath = Path.Combine(SystemDefaults.DefaultBookPhotoPath, name);

                var content = System.IO.File.ReadAllBytes(fullPath);

                return File(content, "pdf/*");
            }
            catch
            {
                return Ok();
            }
        }

        /*[HttpGet]
        public IActionResult PhotoModal(int bookId)
        {
            string name = _categoryService.Get(bookId).Result.Data.Photo;
            return PartialView("PhotoModal", "Picture?name=" + name);
        }*/

        /*private byte[] GetBytes(IFormFile file)
        {
            using var memorystream = new MemoryStream();
            file.CopyTo(memorystream);

            return memorystream.ToArray();
        }
*/

        /* private string Photo()
         {
             if (Request.Form.Files.Count <= 0)
                 return null;

             var file = Request.Form.Files[0];
             byte[] content = GetBytes(file);
             return _fileService.SecureSaveFile(content, "jpg", SystemDefaults.DefaultBookPhotoPath);
         }*/
    }
}