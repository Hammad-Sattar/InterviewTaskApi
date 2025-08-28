using InterviewTask.Models;
using InterviewTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTask.Controllers
    {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
        {
        private readonly IBookRepository _repo;

        // Hardcoded authors list
        private readonly List<Author> authors = new List<Author>
        {
            new Author { Id = 1, Name = "J.K. Rowling" },
            new Author { Id = 2, Name = "George R.R. Martin" },
            new Author { Id = 3, Name = "J.R.R. Tolkien" },
            new Author { Id = 4, Name = "Robert C. Martin" }
        };

        public BooksController(IBookRepository repo)
            {
            _repo = repo;
            }

        [HttpGet]
        public async Task<IActionResult> GetAll()
            {
            var books = await _repo.GetAllAsync();
            return Ok(books);
            }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
            {
            var book = await _repo.GetByIdAsync(id);
            return book == null ? NotFound($"Book with ID {id} not found.") : Ok(book);
            }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
            {
            if (book == null || string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Book or Title cannot be empty.");

            var createdBook = await _repo.AddAsync(book);
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
            {
            if (book == null || id != book.Id)
                return BadRequest("Book is null or ID mismatch.");

            var existing = await _repo.GetByIdAsync(id);
            if (existing == null)
                return NotFound($"Book with ID {id} not found.");

            var updated = await _repo.UpdateAsync(book);
            return Ok(updated);
            }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
            {
            var success = await _repo.DeleteAsync(id);
            return success ? NoContent() : NotFound($"Book with ID {id} not found.");
            }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
            {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title query is required.");

            var books = await _repo.SearchByTitleAsync(title);
            return books.Count == 0 ? NotFound("No books found matching the title.") : Ok(books);
            }

        [HttpGet("authors")]
        public IActionResult GetAuthors()
            {
            return Ok(authors);
            }
        }
    }
