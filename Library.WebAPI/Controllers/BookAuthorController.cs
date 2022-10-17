using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;
        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Add(BookAuthor bookAuthor)

        {
            var result = _bookAuthorService.Add(bookAuthor);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Update(BookAuthor bookAuthor)

        {
            var result = _bookAuthorService.Update(bookAuthor);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()

        {
            var result = _bookAuthorService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid/{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _bookAuthorService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _bookAuthorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
