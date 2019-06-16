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
    public class EfAddAuthorCommand : IAddAuthorCommand
    {
        private readonly BookstoreContext _context;
        public void Execute(AuthorDto request)
        {
            if (_context.Authors.Any(a => a.FirstName == request.FirstName && a.LastName == request.LastName))
            {
                throw new EntityAlreadyExistsException("Author already exists.");
            }
            Author author = new Author
            {
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            _context.Authors.Add(author);
        }
    }
}
