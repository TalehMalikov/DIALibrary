using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;

        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        public async Task<IActionResult> GetAll()
        {
            FileViewModel model = new FileViewModel
            {
                Files = await _fileService.GetAllFiles() 
            };
            return View(model);
        }

        public async Task<IActionResult> GetFileByCategoryId(int id)
        {
            FileViewModel model = new FileViewModel
            {
                Files = await _fileService.GetAllFilesByCategoryId(id)
            };

            return View(model);
        }

        public async Task<IActionResult> GetById(int id)
        {
            FileViewModel model = new FileViewModel
            {
                File = await _fileService.GetFileById(id)
            };

            return View(model);
        }

    }
}
