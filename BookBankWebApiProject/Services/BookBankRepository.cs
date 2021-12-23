using BookBankLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.Services
{
    public class BookBankRepository : IBookBankRepository
    {
        private readonly BookBankContext _context;

        public BookBankRepository(BookBankContext context)
        {
            _context = context;
        }
        public void AddAuthor(Author author)
        {
            _context.Authors.Add(author);
        }

        public async Task AddBook(string authorId, Book book)
        {
            var author = await GetAuthorById(authorId, false);
            author.Books.Add(book);
        }

        public async Task<bool> AuthorExists(string authorId)
        {
            return await _context.Authors.AnyAsync<Author>(e => e.AuthorId == authorId);
        }

        public async Task<bool> BookExists(string bookIsbn)
        {
            return await _context.Books.AnyAsync<Book>(e => e.BookIsbn == bookIsbn);
        }

        public void DeleteAuthor(Author author)
        {
            _context.Authors.Remove(author);
        }

        public void DeleteBook(Book book)
        {
            _context.Books.Remove(book);
        }

        public async Task<Author> GetAuthorById(string authorId, bool includeBooks)
        {
            IQueryable<Author> result;

            if (includeBooks)
            {
                result = _context.Authors.Include(c => c.Books)
                    .Where(c => c.AuthorId == authorId);
            }
            else result = _context.Authors.Where(c => c.AuthorId == authorId);

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            var result = _context.Authors.OrderBy(c => c.AuthorName);
            return await result.ToListAsync();
        }

        public async Task<Book> GetBookById(string authorId, string bookIsbn)
        {
            IQueryable<Book> result = _context.Books.Where(p => p.AuthorId == authorId && p.BookIsbn == bookIsbn);
            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksOfAuthor(string authorId)
        {
            IQueryable<Book> result = _context.Books.Where(p => p.AuthorId == authorId);
            return await result.ToListAsync();
        }

        public void PutAuthor(Author author)
        {
            _context.Entry(author).State = EntityState.Modified;
        }

        public void PutBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
        }

        public async Task<bool> Save()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
