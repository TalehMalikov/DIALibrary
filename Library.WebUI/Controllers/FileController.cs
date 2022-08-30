using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;

        public FileController(IFileService fileService, ICategoryService categoryService)
        {
            _fileService = fileService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ShowAllFiles()
        {
            FileViewModel model = new FileViewModel
            {
                Files = await _fileService.GetAllFiles()
            };
            return View(model);
        }

        public async Task<IActionResult> GetAllFilesByCategoryId(int id)
        {
            CategoryFileViewModel model = new CategoryFileViewModel
            {
                FileModel = new FileViewModel
                {
                    Files = await _fileService.GetAllFilesByCategoryId(id)
                },
                CategoryModel = new CategoryViewModel
                {
                    CategoryList = await _categoryService.GetAll()
                }
            };

            return View(model);
        }

        public async Task<IActionResult> ShowFileInfo(int id)
        {
            FileViewModel model = new FileViewModel
            {
                FileAuthor = await _fileService.GetFileWithAuthors(id)
            };

            return View(model);
        }


        public async Task<IActionResult> SearchByName(FileViewModel model)
        {
            var result = await _fileService.GetAllFiles();
            var filteredData = result.Data.Where(p => p.Name.ToLower().Contains(model.Name.ToLower())).ToList();
            return View(filteredData);
        }
    }
}
