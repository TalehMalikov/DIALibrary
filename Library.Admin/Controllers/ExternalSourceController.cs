using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class ExternalSourceController : Controller
    {
        private readonly IExternalSourceService _externalSource;

        public ExternalSourceController(IExternalSourceService externalSource)
        {
            _externalSource = externalSource;
        }

        public async Task<IActionResult> Index()
        {
            if (String.IsNullOrWhiteSpace(HttpContext.Session.GetString("AdminAccessToken")))
                return RedirectToAction("Login", "Authentication");
            var model = new ExternalSourceViewModel();
            var result = await _externalSource.GetAll();
            model.SourceList = result.Data;
            return View(model);
        }

        
    }
}
