using BookstoreAplication.Commands;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfDeleteBookCommand : IDeleteBookCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteBookCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var accessory = _context.Accessories.Find(request);

            if (accessory == null)
                throw new EntityNotFoundException("Book not found");

            _context.Accessories.Remove(accessory);
            _context.SaveChanges();
        }
    }
}
