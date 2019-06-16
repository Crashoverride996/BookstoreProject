using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using BookstoreAplication.Searches;
using EfCommands;
using EfDataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private IGetBooksCommand _getBooks;
        private IGetBookCommand _getBook;
        private IAddBookCommand _addBook;
        private IDeleteBookCommand _delBook;
        private IEditBookCommand _editBook;

        public BooksController(IGetBooksCommand getBooks, IGetBookCommand getBook, IAddBookCommand addBook, IDeleteBookCommand delBook, IEditBookCommand editBook)
        {
            _getBooks = getBooks;
            _getBook = getBook;
            _addBook = addBook;
            _delBook = delBook;
            _editBook = editBook;
        }

        // GET: api/Books
        [HttpGet]
        public IActionResult Get([FromQuery] BookSearch search)
        {
            var books = _getBooks.Execute(search);

            return Ok(books);
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            try
            {
                var book = _getBook.Execute(id);
                return Ok(book);
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Book not found")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);

            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // POST: api/Books
        [HttpPost]
        public IActionResult Post([FromBody] BookDto book)
        {
            try
            {
                _addBook.Execute(book);
                return NoContent();
            }
            catch  (Exception)
            {
                return NotFound();
            }
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateBookDto book)
        {
            book.Id = id;
            try
            {

                _editBook.Execute(book);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Book doesn't exist.")
                {
                    return NotFound(e.Message);
                }

                return UnprocessableEntity(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _delBook.Execute(id);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }
    }
}
