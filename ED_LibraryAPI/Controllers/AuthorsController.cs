using ED_LibraryAPI.Services;
using ED_LibraryAPI.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;

namespace ED_LibraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] //api/authors
    public class AuthorsController : ControllerBase
    {
        private IAuthorService _service;
        public AuthorsController(IAuthorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<List<AuthorDTO>>> Get()
        {
            var response = await _service.GetAllAuthors();
            return Ok(response);
        }

        [HttpGet, Route("withbooks")] //api/authors/withbooks
        public async Task<ActionResult<List<AuthorWithBooksDTO>>> GetWithBooks()
        {
            var response = await _service.GetAllAuthorsWithBooks();
            return Ok(response);
        }

        [HttpGet, Route("{id}")] //api/authors/1
        public async Task<ActionResult<BookDTO>> Get(int id)
        {
            var response = await _service.GetAuthorById(id);

            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpGet, Route("withbooks/{id}")] //api/authors/withbooks/1
        public async Task<ActionResult<AuthorWithBooksDTO>> GetByIdWithBooks ([FromRoute] int id)
        {
            var response = await _service.GetAuthorWithBooksById(id);

            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpGet, Route("search")] //api/authors/search
        public async Task<ActionResult<List<AuthorDTO>>> Search(string? firstName, string? lastName)
        {
            var response = await _service.SearchAuthor(firstName!, lastName!);
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AuthorDTO>> Post([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var response = await _service.AddAuthor(authorDTO);
                return Ok(response);
            } catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult<AuthorDTO>> Put([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var response = await _service.ReplaceAuthor(authorDTO);
                return Ok(response);
            }
            catch (BadRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPatch]
        public async Task<ActionResult<AuthorDTO>> Patch([FromBody] AuthorDTO authorDTO)
        {
            try
            {
                var response = await _service.UpdateAuthor(authorDTO);
                return Ok(response);
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete, Route("{id}")]
        public async Task<ActionResult<bool>> Delete([FromRoute] int id)
        {
            var response = await _service.DeleteAuthor(id);

            if (response) return Ok(response);
            else return BadRequest(response);
        }

    }
}
