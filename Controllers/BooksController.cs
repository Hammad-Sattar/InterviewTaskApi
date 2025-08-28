using InterviewTask.Models;
using InterviewTask.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace InterviewTask.Controllers
    {
    [ApiController]
    [Route("api/[controller]")]
    public class BooksController : ControllerBase
        {
        private readonly IBookRepository _repo;

       
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
            return book == null ? NotFound() : Ok(book);
            }

        [HttpPost]
        public async Task<IActionResult> Create(Book book)
            {
            if (string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Title is required.");

            var createdBook = await _repo.AddAsync(book); 
            return CreatedAtAction(nameof(GetById), new { id = createdBook.Id }, createdBook);
            }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Book book)
            {
            if (id != book.Id) return BadRequest("ID mismatch.");

            var updated = await _repo.UpdateAsync(book);
            return updated == null ? NotFound() : Ok(updated);
            }
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
            {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest("Title is required.");

            var books = await _repo.SearchByTitleAsync(title);

            if (books.Count == 0)
                return NotFound("No books found matching the title.");

            return Ok(books);
            }

        [HttpGet("authors")]
        public IActionResult GetAuthors()
            {
            return Ok(authors);
            }
        }
    }
