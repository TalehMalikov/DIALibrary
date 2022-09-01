using Library.Business.Abstraction;
using Library.Entities.Concrete;
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
