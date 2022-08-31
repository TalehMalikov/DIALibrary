using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using File = Library.Entities.Concrete.File;

namespace Library.WebUI.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;

        public FileController(IFileService fileService, ICategoryService categoryService,IFileTypeService fileTypeService)
        {
            _fileService = fileService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
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


        public async Task<IActionResult> SearchByName(FileViewModel data)
        {
            var result = await _fileService.GetAllFiles();
            var filteredData = result.Data.Where(p => p.Name.ToLower().Contains(data.Name.ToLower())).ToList();
            FileViewModel model = new FileViewModel
            {
                Files = new SuccessDataResult<List<File>>(filteredData)
            };
            return View(model);
        }

        public async Task<IActionResult> ShowFileteredFiles(int id)
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
                FileAuthor = await _fileService.GetFileWithAuthors(id)
            };

            return View(model);
        }

        public async Task<IActionResult> FilterByCataloq()
        {
            var model = new FileViewModel();
            return View(model);
        }


        public async  Task<IActionResult> E_Catalog()
        {
            return View();
        }
    }
}
