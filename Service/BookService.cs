using InterviewTask.Models;
using InterviewTask.Repositories;

namespace InterviewTask.Service
    {
    public class BookService : IBookService
        {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
            {
            _bookRepository = bookRepository;
            }

        public async Task<List<Book>> GetAllAsync()
            {
            return await _bookRepository.GetAllAsync();
            }

        public async Task<Book?> GetByIdAsync(int id)
            {
            return await _bookRepository.GetByIdAsync(id);
            }

        public async Task<Book> AddAsync(Book book)
            {
            return await _bookRepository.AddAsync(book);
            }

        public async Task<Book?> UpdateAsync(Book book)
            {
            return await _bookRepository.UpdateAsync(book);
            }

        public async Task<bool> DeleteAsync(int id)
            {
            return await _bookRepository.DeleteAsync(id);
            }

        public async Task<List<Book>> SearchByTitleAsync(string title)
            {
            return await _bookRepository.SearchByTitleAsync(title);
            }
        }
    }
