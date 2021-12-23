using BookBankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.Services
{
    public interface IBookBankRepository
    {
        Task<bool> BookExists(string bookIsbn);
        Task<bool> AuthorExists(string authorId);
        Task<IEnumerable<Book>> GetBooksOfAuthor(string authorId);
        Task<Book> GetBookById(string authorId, string bookIsbn);
        Task AddBook(String authorId, Book book);
        void PutBook(Book book);
        void DeleteBook(Book book);
        Task<IEnumerable<Author>> GetAuthors();
        Task<Author> GetAuthorById(string authorId, bool includeBooks);
        void AddAuthor(Author author);
        void PutAuthor(Author author);
        void DeleteAuthor(Author author);
        Task<bool> Save();
    }
}
