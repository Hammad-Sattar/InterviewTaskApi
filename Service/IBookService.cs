﻿using InterviewTask.Models;

namespace InterviewTask.Service
    {
    public interface IBookService
        {
        Task<List<Book>> GetAllAsync();
        Task<Book?> GetByIdAsync(int id);
        Task<Book> AddAsync(Book book);
        Task<Book?> UpdateAsync(Book book);
        Task<bool> DeleteAsync(int id);
        Task<List<Book>> SearchByTitleAsync(string title);
        }
    }
