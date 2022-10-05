using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        private readonly ISectorService _sectorService;
        private readonly ISpecialtyService _specialtyService;
        public GroupController(IGroupService groupService, ISectorService sectorService, ISpecialtyService specialtyService)
        {
            _groupService = groupService;
            _sectorService = sectorService;
            _specialtyService = specialtyService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _groupService.GetAll(token);
            var sectorList = await _sectorService.GetAll(token);
            var specialtyList = await _specialtyService.GetAll(token);
            var model = new GroupViewModel
            {
                Groups = result.Data,
                SectorList = new SelectList(sectorList.Data, "Id", "Name"),
                SpecialtyList = new SelectList(specialtyList.Data, "Id", "Name")
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> SaveGroup(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            var model = new GroupViewModel();
            var sectorList = await _sectorService.GetAll(accessToken);
            var specialtyList = await _specialtyService.GetAll(accessToken);
            model.SectorList =
            model.SpecialtyList = new SelectList(specialtyList.Data, "Id", "Name");
            if (id == 0)
                return PartialView(model);
            var group = await _groupService.Get(accessToken, id);
            model.Group = group.Data;
            return PartialView(model);
        }
    }
}
