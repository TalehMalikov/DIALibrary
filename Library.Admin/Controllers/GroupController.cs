using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class GroupController : Controller
    {
        private readonly IGroupService _groupService;
        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        public async Task<IActionResult>Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _groupService.GetAll(token);
            var model = new GroupViewModel
            {
                Groups = result.Data
            };
            return View(model);
        }
    }
}
