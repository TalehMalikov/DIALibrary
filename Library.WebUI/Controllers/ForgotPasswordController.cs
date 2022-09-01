using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class ForgotPasswordController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
