using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using Domain;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetBookCommand : IGetBookCommand
    {
        private readonly BookstoreContext _context;

        public EfGetBookCommand(BookstoreContext context)
        {
            _context = context;
        }

        public BookDto Execute(int request)
        {
            var book = _context.Books.Find(request);

            if (book == null)
                throw new EntityNotFoundException("Book not found");

            return new BookDto
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price
            };
        }
    }
}
