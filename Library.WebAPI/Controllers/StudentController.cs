using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin,Admin,GroupAdmin")]
        public IActionResult Add(Student student)
        {
            var result = _studentService.Add(student);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public IActionResult Update(Student student)
        {
            var result = _studentService.Update(student);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("getall")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _studentService.GetAll();
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
            var result = _studentService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyuserid/{id:int}")]
        [Authorize]
        public IActionResult GetByUserId(int id)
        {
            var result = _studentService.GetByUserId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin,GroupAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _studentService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
