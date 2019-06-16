using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetAuthorCommand : IGetAuthorCommand
    {
        private readonly BookstoreContext _context;

        public EfGetAuthorCommand(BookstoreContext context)
        {
            _context = context;
        }

        public AuthorDto Execute(int request)
        {
            var author = _context.Authors.Find(request);

            if (author == null)
                throw new EntityNotFoundException("Author not found");

            return new AuthorDto
            {
                FirstName = author.FirstName,
                LastName = author.LastName
            };
        }
    }
}
