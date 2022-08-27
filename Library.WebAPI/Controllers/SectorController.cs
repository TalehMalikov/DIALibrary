using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SectorController : ControllerBase
    {
        private readonly ISectorService _sectorService;
        public SectorController(ISectorService sectorService)
        {
            _sectorService = sectorService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize("SuperAdmin,Admin")]
        public IActionResult Add(Sector sector)
        {
            var result = _sectorService.Add(sector);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize("SuperAdmin,Admin")]
        public IActionResult Update(Sector sector)
        {
            var result = _sectorService.Update(sector);
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
            var result = _sectorService.GetAll();
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
            var result = _sectorService.Get(id);
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
            var result = _sectorService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
