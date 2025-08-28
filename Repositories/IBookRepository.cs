using InterviewTask.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterviewTask.Repositories
    {
    public interface IBookRepository
        {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<List<Book>> SearchByTitleAsync(string title);

        }
    }
