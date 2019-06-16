using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfEditBookCommand : IEditBookCommand
    {
        private readonly BookstoreContext _context;

        public EfEditBookCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(CreateBookDto request)
        {
            var book = _context.Books.Find(request.Id);

            if (book == null)
            {
                throw new EntityNotFoundException("Book doesn't exist.");
            }

            book.Title = request.Title;
            book.Description = request.Description;
            book.Price = request.Price;
            book.ImagePath = "";
            book.AuthorId = (int)request.AuthorId;

            _context.SaveChanges();
        }
    }
}
