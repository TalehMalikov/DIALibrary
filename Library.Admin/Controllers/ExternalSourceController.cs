using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class ExternalSourceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
