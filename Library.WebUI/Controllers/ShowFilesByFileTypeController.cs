using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class ShowFilesByFileTypeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IFileTypeService _fileTypeService;

        public ShowFilesByFileTypeController(IFileService fileService,IFileTypeService fileTypeService)
        {
            _fileService = fileService;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> Index(int id)
        {
            FileViewModel viewModel = new FileViewModel();

            viewModel.Files = await _fileService.GetFilesByFileTypeId(id);
            var fileTypes = await _fileTypeService.GetAllFileTypes();

            ViewBag.AllFileTypes = fileTypes.Data;

            return View(viewModel);
        }

        public async Task<IActionResult> ShowFileInfoFileType(int id)
        {
            FileViewModel model = new FileViewModel
            {
                File = await _fileService.GetFileById(id)
            };

            return View(model);
        }
    }
}
