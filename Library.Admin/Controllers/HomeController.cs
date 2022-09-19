using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        { 
            if (String.IsNullOrWhiteSpace(HttpContext.Session.GetString("AdminAccessToken")))
                return RedirectToAction("Login", "Authentication");

            return View();
        }
    }
}
