using Library.Business.Abstraction;
using Library.Core.Result.Concrete;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
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
        public IActionResult Add(FileAuthorDtoForCrud bookAuthor)

        {
            var result = _bookAuthorService.Add(bookAuthor);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("addlist")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult AddList(List<FileAuthorDtoForCrud> bookAuthors)

        {
            var result = _bookAuthorService.AddList(bookAuthors);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Update(FileAuthorDtoForCrud bookAuthor)

        {
            var result = _bookAuthorService.Update(bookAuthor);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost]
        [Route("addallfileauthors")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult AddAllFileAuthor(FileAuthorDto fileAuthorDto)
        {
            var result = _bookAuthorService.AddAllFilesAuthor(fileAuthorDto.AuthorIds, fileAuthorDto.FileId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("deletefileauthor/{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin,GroupAdmin")]
        public IActionResult DeleteFileAuthor(int fileId)
        {
            var result = _bookAuthorService.DeleteFileAuthor(fileId);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPut]
        [Route("updatelist")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult UpdateList(List<FileAuthorDtoForCrud> bookAuthors)

        {
            var result = _bookAuthorService.UpdateList(bookAuthors);
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

        [HttpGet("getallfileauthors/{id:int}")]
        public IActionResult GetAllFileAuthors(int id)
        {
            var result = _bookAuthorService.GetAllFileAuthors(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
