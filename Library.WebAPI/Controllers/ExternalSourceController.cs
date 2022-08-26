using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExternalSourceController : ControllerBase
    {
        private readonly IExternalSourceService _externalSourceService;
        public ExternalSourceController(IExternalSourceService externalSourceService)
        {
            _externalSourceService = externalSourceService;
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(ExternalSource externalSource)
        {
            var result = _externalSourceService.Add(externalSource);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(ExternalSource externalSource)
        {
            var result = _externalSourceService.Update(externalSource);
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
            var result = _externalSourceService.GetAll();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id:int}")]
        public IActionResult Get(int id)
        {
            var result = _externalSourceService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            var result = _externalSourceService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
