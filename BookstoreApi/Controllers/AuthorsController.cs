using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EfDataAccess;
using Domain;
using BookstoreAplication.DTO;
using BookstoreAplication.Commands;
using BookstoreAplication.Searches;
using BookstoreAplication.Exceptions;

namespace BookstoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private IGetAuthorsCommand _getAuthors;
        private IGetAuthorCommand _getAuthor;
        private IAddAuthorCommand _addAuthor;
        private IDeleteAuthorCommand _delAuthor;
        private IEditAuthorCommand _editAuthor;

        public AuthorsController(IGetAuthorsCommand getAuthors, IGetAuthorCommand getAuthor, IAddAuthorCommand addAuthor, IDeleteAuthorCommand delAuthor, IEditAuthorCommand editAuthor)
        {
            _getAuthors = getAuthors;
            _getAuthor = getAuthor;
            _addAuthor = addAuthor;
            _delAuthor = delAuthor;
            _editAuthor = editAuthor;
        }

        // GET api/authors
        [HttpGet]
        public IActionResult Get([FromQuery] AuthorSearch search)
        {
            var authors = _getAuthors.Execute(search);

            return Ok(authors);
        }

        // GET api/authors/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var author = _getAuthor.Execute(id);
            return Ok(author);
        }

        // POST api/authors
        [HttpPost]
        public IActionResult Post([FromBody] AuthorDto author)
        {
            try
            {
                _addAuthor.Execute(author);
                return NoContent();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT api/authors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CreateAuthorDto author)
        {
            author.Id = id;
            try
            {

                _editAuthor.Execute(author);
                return NoContent();
            }
            catch (EntityNotFoundException e)
            {
                if (e.Message == "Author doesn't exist.")
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

        // DELETE api/authors/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _delAuthor.Execute(id);
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