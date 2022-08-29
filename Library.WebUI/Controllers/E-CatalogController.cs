using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class E_CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
