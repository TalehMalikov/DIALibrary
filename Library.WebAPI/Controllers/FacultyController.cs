using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize("SuperAdmin,Admin")]
        public IActionResult Add(Faculty faculty)
        {
            var result = _facultyService.Add(faculty);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize("SuperAdmin,Admin")]
        public IActionResult Update(Faculty faculty)
        {
            var result = _facultyService.Update(faculty);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getall")]
        //[Authorize]
        public IActionResult GetAll()
        {
            var result = _facultyService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var result = _facultyService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize("SuperAdmin,Admin")]
        public IActionResult Delete(int id)
        {
            var result = _facultyService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
