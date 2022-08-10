using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Http;
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
         public IActionResult Update(Faculty faculty)
         {
             var result = _facultyService.Update(faculty);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
         }

         [HttpDelete]
         public IActionResult Delete(Faculty faculty)
         {
             var result = _facultyService.Delete(faculty);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
         }

         [HttpGet]
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
         public IActionResult Update(int id)
         {
             var result = _facultyService.Get(id);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
         }
    }
}
