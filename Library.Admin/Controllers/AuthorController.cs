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

        public async Task<IActionResult> Index()
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
        public async Task<IActionResult> Add(AuthorViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _authorService.Add(token, model.Author);
                return RedirectToAction("Index");
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _authorService.Delete(token, id);
                return RedirectToAction("Index");
            }
            return new NotFoundResult();
        }
    }
}
