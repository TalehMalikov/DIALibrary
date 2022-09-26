using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class FacultyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
