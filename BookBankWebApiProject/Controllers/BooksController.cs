using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookBankLibrary.Models;
using BookBankWebApiProject.Services;
using AutoMapper;
using BookBankWebApiProject.DTOs;

namespace BookBankWebApiProject.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IBookBankRepository _bookBankRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookBankRepository bookBankRepository, IMapper mapper)
        {
            _bookBankRepository = bookBankRepository;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet("{authorId}/books")]

        public async Task<ActionResult<Book>> GetBooks(string authorId)
        {
            if (!(await _bookBankRepository.AuthorExists(authorId)))
            {
                return NotFound();
            }

            var BooksOfAuthor = await _bookBankRepository.GetBooksOfAuthor(authorId);
            var BooksOfAuthorResults = _mapper.Map<IEnumerable<BookDto>>(BooksOfAuthor);

            return Ok(BooksOfAuthorResults);
        }

        // GET api/<controller>/5

        [HttpGet("{authorId}/books/{id}")]
        public async Task<ActionResult<Book>> GetBook(string authorId, string id)
        {
            if (!await _bookBankRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var book = await _bookBankRepository.GetBookById(authorId, id);

            if (book == null)
            {
                return NotFound();
            }

            var bookResult = _mapper.Map<BookDto>(book);
            return Ok(bookResult);
        }

        // POST api/<controller>

        [HttpPost("{authorId}/books")]
        public async Task<ActionResult<BookDto>> CreateBook(string authorId, [FromBody] BookForCreationDto book)
        {
            if (book == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _bookBankRepository.AuthorExists(authorId)) return NotFound();

            var finalBook = _mapper.Map<Book>(book);

            await _bookBankRepository.AddBook(authorId, finalBook);


            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdBookToReturn = _mapper.Map<BookDto>(finalBook);

            return CreatedAtAction("GetBook", new { authorId = authorId, id = createdBookToReturn.BookIsbn }, createdBookToReturn);
        }

        // PUT api/<controller>/5

        [HttpPut("{authorId}/books/{id}")]
        public async Task<ActionResult> UpdateBook(string authorId, string id, [FromBody] BookForUpdateDto book)
        {
            if (book == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (!await _bookBankRepository.AuthorExists(authorId)) return NotFound();

            Book oldBookEntity = await _bookBankRepository.GetBookById(authorId, id);

            if (oldBookEntity == null) return NotFound();

            _mapper.Map(book, oldBookEntity);


            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // DELETE api/<controller>/5

        [HttpDelete("{authorId}/books/{id}")]
        public async Task<ActionResult> DeleteBook(string authorId, string id)
        {
            if (!await _bookBankRepository.AuthorExists(authorId)) return NotFound();

            Book bookEntity2Delete = await _bookBankRepository.GetBookById(authorId, id);
            if (bookEntity2Delete == null) return NotFound();

            _bookBankRepository.DeleteBook(bookEntity2Delete);

            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
