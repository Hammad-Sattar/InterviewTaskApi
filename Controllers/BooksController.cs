using InterviewTask.Models;
using InterviewTask.Service;
using Microsoft.AspNetCore.Mvc;
namespace InterviewTask.Controllers
    {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
        {
        private readonly IBookService _service;

        // Hardcoded authors list
        private readonly List<Author> authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling" },
            new Author { Id = 2, Name = "George R.R. Martin" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" },
            new Author { Id = 4, Name = "Robert C. Martin" }
        };

        public BooksController(IBookService service)
            {
            _service = service;
            }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            {
            var books = await _service.GetAllAsync();
            return Ok(books);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            {
            var book = await _service.GetByIdAsync(id);
            return book == null ? NotFound($"Book with ID {id} not found.") : Ok(book);
            }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
            {
            if (book == null || string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Book or Title cannot be empty.");

            var createdBook = await _service.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
            {
            if (book == null || id != book.Id)
                return BadRequest("Book is null or ID mismatch.");

            var existing = await _service.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Book with ID {id} not found.");

            var updated = await _service.UpdateAsync(book);
            return Ok(updated);
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound($"Book with ID {id} not found.");
            }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
            {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title query is required.");

            var books = await _service.SearchByTitleAsync(title);
            return books.Count == 0 ? NotFound("No books found matching the title.") : Ok(books);
            }

        [HttpGet("authors")]
        public IActionResult GetAuthors()
            {
            return Ok(authors);
            }
        }
    }
