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
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IBookBankRepository _bookBankRepository;
        private readonly IMapper _mapper;

        public AuthorsController(IBookBankRepository bookBankRepository, IMapper mapper)
        {
            _bookBankRepository = bookBankRepository;
            _mapper = mapper;
        }

        // GET: api/<controller>
        [HttpGet]
        [Route("/api/authors")]
        public async Task<ActionResult<Author>> GetAuthors()
        {
            var authors = await _bookBankRepository.GetAuthors();

            var results = _mapper.Map<IEnumerable<AuthorWithoutBooksDto>>(authors);

            return Ok(results);
        }

        // GET api/<controller>/5

        [HttpGet("{id}")]
        public async Task<ActionResult<Author>> GetAuthorById(string id, bool includeBooks = false)
        {
            var author = await _bookBankRepository.GetAuthorById(id, includeBooks);

            if (author == null)
            {
                return NotFound();
            }

            if (includeBooks)
            {
                var authorResult = _mapper.Map<AuthorDto>(author);

                return Ok(authorResult);
            }

            var authorWithoutBooksResult = _mapper.Map<AuthorWithoutBooksDto>(author);

            return Ok(authorWithoutBooksResult);
        }

        // PUT: api/Authors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(string id, [FromBody] AuthorForUpdationDto author)
        {
            if (author == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            Author oldAuthorEntity = await _bookBankRepository.GetAuthorById(id, false);

            if (oldAuthorEntity == null) return NotFound();

            _mapper.Map(author, oldAuthorEntity);


            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }

        // POST: api/Authors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Author>> PostAuthor([FromBody] AuthorForCreationDto author)
        {
            if (author == null) return BadRequest();

            if (!ModelState.IsValid) return BadRequest(ModelState);

            var finalAuthor = _mapper.Map<Author>(author);

            _bookBankRepository.AddAuthor(finalAuthor);


            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            var createdAuthorToReturn = _mapper.Map<Author>(author);

            return CreatedAtAction("GetAuthorById", new { id = createdAuthorToReturn.AuthorId }, createdAuthorToReturn);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(string id)
        {

            Author authorEntity2Delete = await _bookBankRepository.GetAuthorById(id, false);
            if (authorEntity2Delete == null) return NotFound();

            _bookBankRepository.DeleteAuthor(authorEntity2Delete);

            if (!await _bookBankRepository.Save())
            {
                return StatusCode(500, "A problem happened while handling your request.");
            }

            return NoContent();
        }
    }
}
