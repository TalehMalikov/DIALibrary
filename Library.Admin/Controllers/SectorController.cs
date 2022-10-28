using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class SectorController : Controller
    {
        private readonly ISectorService _sectorService;

        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var model = new SectorViewModel();
            var result = await _sectorService.GetAll(token);
            model.Sectors = result.Data;
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SaveSector(int id)
        {
            if (id == 0)
                return PartialView();
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var sector = await _sectorService.Get(token, id);
            var model = new SectorViewModel
            {
                Sector = sector.Data
            };
            return PartialView(model);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> SaveSector(SectorViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Sector.Id == 0)
            {
                await _sectorService.Add(token, model.Sector);
                return RedirectToAction("Index");
            }

            await _sectorService.Update(token, model.Sector);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            await _sectorService.Delete(token, id);
            return RedirectToAction("Index");
        }
    }
}
