using ED_LibraryAPI.DTO;
using ED_LibraryAPI.Services;
using Microsoft.AspNetCore.Mvc;


namespace ED_LibraryAPI.Controllers
{
    [Route("api/[controller]")] //url/api/books
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookService _service;
        public BooksController(IBookService service)
        {
            _service = service;
        }

        [HttpGet] 
        public async Task<List<BookDTO>> Get() //url/api/books
        {
            return await _service.GetAllBooks();
        }

        [HttpGet, Route("{id}")] //url/api/books/1
        public async Task<ActionResult<BookDTO>> Get([FromRoute] int id)
        {
            var dto = await _service.GetBook(id);
            if (dto == null) return NotFound("Invalid iD");
            return Ok(dto);
        }

        [HttpGet, Route("search")]
        public async Task<ActionResult<List<BookDTO>>> Search
            (string? name, string? publisher, string? authorFirst, string? authorLast)
        {
            var response = await _service.Search(name, publisher, authorFirst, authorLast);
            if (response == null) return BadRequest("No book matches the criteria");
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Post([FromBody] BookDTO dto)
        {
            BookDTO? result = await _service.AddBook(dto);
            if (result is null) return BadRequest("The author was not found");
            else return Ok(result);
        }

        [HttpPatch, Route("{id}")] //url/api/books -- PATCH
        public async Task<ActionResult<BookDTO>> Patch([FromRoute] int id, [FromBody] BookDTO dto)
        {
            var response = await _service.UpdateBook(id, dto);
            if (response is null) return BadRequest("Book could not be found");
            return Ok(response);
        }

        [HttpPut, Route("{id}")]
        public async Task<ActionResult<BookDTO>> Put([FromRoute] int id, [FromBody] BookDTO dto)
        {
            var response = await _service.Replace(id, dto);
            if (response is null) return BadRequest("Book could not be found");
            return Ok(response);
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<bool>> Delete (int id)
        {
            return await _service.Delete(id);
        }

    }
}
