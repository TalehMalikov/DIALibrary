using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class OurLibraryController : Controller
    {
        private readonly IExternalSourceService _externalSourceService;

        public OurLibraryController(IExternalSourceService externalSourceService)
        {
            _externalSourceService = externalSourceService;
        }

        public IActionResult MoreInfo()
        {
            return View();
        }

        public async Task<IActionResult>ExternalSource()
        {
            ExternalSourceViewModel viewModel = new ExternalSourceViewModel();
            viewModel.ExternalSourceList = await _externalSourceService.GetAll();
            return View(viewModel);
        }
    }
}
