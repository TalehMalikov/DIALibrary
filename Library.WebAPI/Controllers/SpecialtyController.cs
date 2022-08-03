using Library.Business.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        public SpecialtyController(ISpecialtyService specialtyService)
        {
            _specialtyService = specialtyService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var specialties = _specialtyService.GetAll();

                return Ok(specialties);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }
    }
}
