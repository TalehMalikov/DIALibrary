using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class ResourceController : Controller
    {
        private readonly IFileService _fileService;
        public ResourceController(IFileService fileService)
        {
            _fileService = fileService;
        }

        #region Books

        public async Task<IActionResult> ShowBooks(int id)
        {
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
            if (id == 0)
                return PartialView();

            var bankModel = await _fileService.Get(id);

            return PartialView(bankModel);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ResourceViewModel viewModel)
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
        public IActionResult Delete(ResourceViewModel viewModel)
        {
            var deletedId = viewModel.DeletedBook.Id;

            _fileService.Delete(deletedId);

            //TempData["Message"] = "Operation successfully";

            return RedirectToAction("ShowBooks");
        }

        #endregion

    }
}
