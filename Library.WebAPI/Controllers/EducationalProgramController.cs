using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EducationalProgramController : ControllerBase
    {
        private readonly IEducationalProgramService _educationalProgramService;

        public EducationalProgramController(IEducationalProgramService educationalProgramService)
        {
            _educationalProgramService = educationalProgramService;
        }

        [HttpGet("getall")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _educationalProgramService.GetAll();
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
            var result = _educationalProgramService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPost("add")]
        [Authorize(Roles = "SuperAdmin, Admin, ResourceAdmin")]
        public IActionResult Add(EducationalProgram value)
        {
            var result = _educationalProgramService.Add(value);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }


        [HttpPut("update")]
        [Authorize(Roles = "SuperAdmin, Admin, ResourceAdmin")]
        public IActionResult Update(EducationalProgram value)
        {
            var result = _educationalProgramService.Update(value);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin, Admin, ResourceAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _educationalProgramService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }

}
