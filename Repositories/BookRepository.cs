﻿using InterviewTask.Data;
using InterviewTask.Models;
using InterviewTask.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BookRepository : IBookRepository
    {
    private readonly AppDbContext _context;

    public BookRepository(AppDbContext context)
        {
        _context = context;
        }

    public async Task<List<Book>> GetAllAsync()
        {
        return await _context.Books.ToListAsync();
        }

    public async Task<Book?> GetByIdAsync(int id)
        {
        return await _context.Books.FindAsync(id);
        }

    public async Task<Book> AddAsync(Book book)
        {
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return book; 
        }

    public async Task<Book?> UpdateAsync(Book book)
        {
        var existingBook = await _context.Books.FindAsync(book.Id);
        if (existingBook == null) return null;

        existingBook.Title = book.Title;
        existingBook.Genre = book.Genre;
       

        await _context.SaveChangesAsync();
        return existingBook;
        }

    public async Task<bool> DeleteAsync(int id)
        {
        var book = await _context.Books.FindAsync(id);
        if (book == null) return false;

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return true;
        }
    public async Task<List<Book>> SearchByTitleAsync(string title)
        {
        return await _context.Books
            .Where(b => b.Title.Contains(title)) 
            .ToListAsync();
        }

    }
