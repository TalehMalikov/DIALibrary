using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
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

        public async Task<IActionResult> Delete(int id)
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
