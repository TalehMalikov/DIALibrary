using Library.Admin.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class ActivityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult AddActivity(ActivityViewModel viewModel)
        //{

        //    return RedirectToAction("Index");
        //}

        public IActionResult AddActivity(IFormCollection collection)
        {
            _ = collection;
            var col = collection as FormCollection;
            var xx = col.GetEnumerator (); ;
            var xx1 = col.Files;
            return RedirectToAction("Index");
        }
    }
}
