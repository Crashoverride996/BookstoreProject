using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfEditAuthorCommand : IEditAuthorCommand
    {
        private readonly BookstoreContext _context;

        public void Execute(CreateAuthorDto request)
        {
            var author = _context.Authors.Find(request.Id);

            if (author == null)
            {
                throw new EntityNotFoundException("Author doesn't exist.");
            }

            author.FirstName = request.FirstName;
            author.LastName = request.LastName;

            _context.SaveChanges();
        }
    }
}
