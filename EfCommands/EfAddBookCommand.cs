using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfAddBookCommand : IAddBookCommand
    {
        private readonly BookstoreContext _context;

        public EfAddBookCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(BookDto request)
        {
            if (_context.Books.Any(b => b.Title == request.Title))
            {
                throw new EntityAlreadyExistsException();
            }
            if (_context.Authors.Any(a => a.FirstName == request.AuthorFirstname && a.LastName == request.AuthorLastname))
            {
                Author author = _context.Authors.Where(a => a.FirstName == request.AuthorFirstname && a.LastName == request.AuthorLastname).First();
                Book book = new Book
                {
                    Title = request.Title,
                    Description = request.Description,
                    Price = request.Price,
                    ImagePath = "",
                    Author = author
                };
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            else
            {
                Author author = new Author
                {
                    FirstName = request.AuthorFirstname,
                    LastName = request.AuthorLastname
                };
                _context.Authors.Add(author);
                Book book = new Book
                {
                    Title = request.Title,
                    Description = request.Description,
                    Price = request.Price,
                    ImagePath = "",
                    Author = author
                };
                _context.Books.Add(book);
                _context.SaveChanges();
            }
        }
    }
}
