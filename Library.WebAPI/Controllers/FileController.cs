using Library.Business.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }

        [HttpPost]
        [Authorize]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Add(Entities.Concrete.File file)
        {
            var result = _fileService.Add(file);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Authorize]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Update(Entities.Concrete.File file)
        {
            var result = _fileService.Update(file);
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
            var result = _fileService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getnewadded")]
        public IActionResult GetNewAdded()
        {
            var result = _fileService.GetNewAdded();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _fileService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getallfilesbycategoryid/{id:int}")]
        public IActionResult GetAllFilesByCategoryId(int id)
        {
            var result = _fileService.GetAllFilesByCategoryId(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [Authorize]
        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin,Admin,ResourceAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _fileService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getfilewithauthors/{id:int}")]
        public IActionResult GetFileWithAuthors(int id)
        {
            var result = _fileService.GetFileWithAuthors(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("getallfileswithauthors")]
        public IActionResult GetAllFilesWithAuthors()
        {
            var result = _fileService.GetAllFilesWithAuthors();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
