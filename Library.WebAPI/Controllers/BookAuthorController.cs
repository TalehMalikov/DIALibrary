﻿using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookAuthorController : ControllerBase
    {
        private readonly IBookAuthorService _bookAuthorService;
        public BookAuthorController(IBookAuthorService bookAuthorService)
        {
            _bookAuthorService = bookAuthorService;
        }

        [HttpPost]
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

        [HttpDelete]
        public IActionResult Delete(BookAuthor bookAuthor)
        {
            var result = _bookAuthorService.Delete(bookAuthor);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}