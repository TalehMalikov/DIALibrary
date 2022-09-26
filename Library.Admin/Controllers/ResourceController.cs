using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IFileService _fileService;
        private readonly ILanguageService _languageService;
        private readonly ICategoryService _categoryService;
        public ResourceController(IFileService fileService,ILanguageService languageService,ICategoryService categoryService)
        {
            _fileService = fileService;
            _languageService = languageService;
            _categoryService = categoryService;
        }

        #region Books

        public async Task<IActionResult> ShowBooks(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            ResourceViewModel viewModel = new ResourceViewModel();
            var result = await _fileService.GetAll();
            switch (id)
            {
                case 0:
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == false).ToList();
                    break;
                case 1:
                    viewModel.Files = result.Data.Where(f => f.ExistingStatus == true).ToList();
                    break;
                default:
                    viewModel.Files = result.Data;
                    break;
            }
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> SaveBook(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            ResourceViewModel viewModel = new ResourceViewModel();
            var categories = await _categoryService.GetAll();
            categories.Data.Add(new Category()
            {
                Name = "Seç",
                Id = 0
            });
            var languages = await _languageService.GetAll(accessToken);
            languages.Data.Add(new Language()
            {
                Name = "Seç",
                Id = 0
            });
            viewModel.CategoryList = new SelectList(categories.Data, "Id", "Name");
            viewModel.LanguageList = new SelectList(languages.Data, "Id", "Name");

            if (id == 0)
                return PartialView(viewModel);

            var file = await _fileService.Get(id);
            
            viewModel.File = file.Data;
            
            return PartialView(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SaveBook(ResourceViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            try
            {
                if (ModelState.IsValid == false)
                {
                    var errors = ModelState.SelectMany(x => x.Value.Errors).Select(x => x.ErrorMessage).ToList();
                    var errorMessage = errors.Aggregate((message, value) =>
                    {
                        if (message.Length == 0)
                            return value;

                        return message + ", " + value;
                    });

                    TempData["Message"] = errorMessage;
                    return RedirectToAction("ShowBooks");
                }
                if(viewModel.File.Id==0)
                {
                    await _fileService.Add(accessToken,viewModel.File);
                }
                else
                {
                    await _fileService.Update(accessToken,viewModel.File);
                }
                //TempData["Message"] = "Operation successfully";
            }
            catch (Exception exc)
            {
                // log exception here

                //TempData["Message"] = "Operation unsuccessfully";
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult DeleteBook(ResourceViewModel viewModel)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var deletedId = viewModel.DeletedBook.Id;

            _fileService.Delete(token,deletedId);

            //TempData["Message"] = "Operation successfully";

            return RedirectToAction("ShowBooks");
        }

        #endregion

    }
}
