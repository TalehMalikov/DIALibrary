using Library.Entities.Dtos;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
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
        public async Task<IActionResult> Index()
        {
            CategoryFileViewModel categoryFileViewModel = new CategoryFileViewModel
            {
                CategoryModel = new CategoryViewModel
                {
                    CategoryList = await _categoryService.GetAll(),
                }
            };
            var newAddedFiles = await _categoryService.GetNewAddedBooks();
            var newAddedFileAuthors = new List<FileAuthorDto>();

            var fileAuthor = await _fileService.GetFileWithAuthors(newAddedFiles.Data[0].Id);
            foreach (var item in newAddedFiles.Data)
            {
                fileAuthor = await _fileService.GetFileWithAuthors(item.Id);
                newAddedFileAuthors.Add(fileAuthor.Data);
            }
            categoryFileViewModel.FileModel = new FileViewModel()
            {
                NewAddedFileAuthorList = newAddedFileAuthors
            };

            return View(categoryFileViewModel);
        }
    }
}
