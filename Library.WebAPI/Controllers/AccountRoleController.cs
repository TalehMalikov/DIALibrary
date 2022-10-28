using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly IAccountRoleService _accountRoleService;
        public AccountRoleController(IAccountRoleService accountRoleService)
        {
            _accountRoleService = accountRoleService;
        }

        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Add(AccountRoleDto accountRole)
        {
            var result = _accountRoleService.Add(accountRole);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut]
        [Route("update")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Update(AccountRoleDto accountRole)
        {
            var result = _accountRoleService.Update(accountRole);
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
            var result = _accountRoleService.GetAll();
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
            var result = _accountRoleService.Get(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpDelete("delete/{id:int}")]
        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _accountRoleService.Delete(id);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getroles")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public IActionResult GetRoles()
        {
            var result = _accountRoleService.GetRoles();
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

    }
}
