using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IFileAuthorService _fileAuthorService;

        public AuthorController(IAuthorService authorService, IFileAuthorService fileAuthorService)
        {
            _authorService = authorService;
            _fileAuthorService = fileAuthorService;
        }

        public async Task<IActionResult> ShowAuthors()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            var result = await _authorService.GetAll(token);
            var model = new AuthorViewModel
            {
                Authors = result.Data
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SaveAuthor(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(accessToken)) return RedirectToAction("Login", "Authentication");
            AuthorViewModel viewModel = new AuthorViewModel();

            if (id == 0)
                return PartialView(viewModel);

            var author = await _authorService.Get(accessToken, id);

            viewModel.Author = author.Data;

            return PartialView(viewModel);

        }

        [HttpPost]
        public async Task<IActionResult> SaveAuthor(AuthorViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            if (model.Author.Id == 0)
            {
                if (String.IsNullOrWhiteSpace(model.Author.FirstName))
                    model.Author.FirstName = String.Empty;
                if (String.IsNullOrWhiteSpace(model.Author.FatherName))
                    model.Author.FatherName = String.Empty;
                if (String.IsNullOrWhiteSpace(model.Author.LastName))
                    model.Author.LastName = String.Empty;
                var result = await _authorService.Add(token, model.Author);
                return RedirectToAction("ShowAuthors");
            }
            else
            {
                if (String.IsNullOrWhiteSpace(model.Author.FirstName))
                    model.Author.FirstName = String.Empty;
                if (String.IsNullOrWhiteSpace(model.Author.FatherName))
                    model.Author.FatherName = String.Empty;
                if (String.IsNullOrWhiteSpace(model.Author.LastName))
                    model.Author.LastName = String.Empty;
                var result = await _authorService.Update(token, model.Author);
                return RedirectToAction("ShowAuthors");
            }
        }

        [HttpGet]
        public async Task<IActionResult> SaveFileAuthor(int fileId)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AuthorViewModel viewModel = new AuthorViewModel();
                var authors = await _authorService.GetAll(accessToken);
                //viewModel.AuthorList = new MultiSelectList(authors.Data, "Id", "FirstName");
                for (int i = 0; i < authors.Data.Count; i++)
                {
                    viewModel.AuthorList.Add(new SelectListItem($"{authors.Data[i].FirstName} {authors.Data[i].LastName}", $"{authors.Data[i].Id}"));
                }
                if (fileId == 0)
                    return PartialView(viewModel);

                var author = await _fileAuthorService.GetFileWithAuthors(fileId);

                viewModel.FileAuthor = new FileAuthorDto()
                {
                    File = author.Data.File
                };
                viewModel.FileAuthor.Authors = author.Data.Authors;

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
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
