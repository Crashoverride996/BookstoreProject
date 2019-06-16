using System;
using System.Collections.Generic;
using System.Text;
using BookstoreAplication.Commands;
using BookstoreAplication.Exceptions;
using EfDataAccess;

namespace EfCommands
{
    public class EfDeleteAuthorCommand : IDeleteAuthorCommand
    {
        private readonly BookstoreContext _context;

        public EfDeleteAuthorCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(int request)
        {
            var author = _context.Authors.Find(request);

            if (author == null)
                throw new EntityNotFoundException();

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
