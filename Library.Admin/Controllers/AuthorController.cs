using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IFileAuthorService _fileAuthorService;

        public AuthorController(IAuthorService authorService,IFileAuthorService fileAuthorService)
        {
            _authorService = authorService;
            _fileAuthorService = fileAuthorService;
        }

        public async Task<IActionResult> ShowAuthors()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _authorService.GetAll(token);
                var model = new AuthorViewModel
                {
                    Authors = result.Data
                };

                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveAuthor(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AuthorViewModel viewModel = new AuthorViewModel();
                
                if (id == 0)
                    return PartialView(viewModel);

                var author = await _authorService.Get(accessToken,id);

                viewModel.Author = author.Data;

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAuthor(AuthorViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                if(model.Author.Id==0)
                {
                    var result = await _authorService.Add(token, model.Author);
                    return RedirectToAction("ShowAuthors");
                }
                else
                {
                    var result = await _authorService.Update(token, model.Author);
                    return RedirectToAction("ShowAuthors");
                }
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveFileAuthor(int fileId)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AuthorViewModel viewModel = new AuthorViewModel();

                if (fileId == 0)
                    return PartialView(viewModel);

                var author = await _fileAuthorService.GetFileWithAuthors(fileId);

                viewModel.FileAuthor.File = author.Data.File;
                viewModel.FileAuthor.Authors = author.Data.Authors;

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveFileAuthor(AuthorViewModel viewModel)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                if (viewModel.FileAuthor.Id == 0)
                {
                    var result = await _fileAuthorService.Add(token, viewModel.FileAuthorForCrud);
                    return RedirectToAction("ShowAuthors");
                }
                else
                {
                    var result = await _fileAuthorService.Update(token, viewModel.FileAuthorForCrud);
                    return RedirectToAction("ShowAuthors");
                }
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> DeleteAuthor(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _authorService.Delete(token, id);
                return RedirectToAction("ShowAuthors");
            }
            return new NotFoundResult();
        }
    }
}
