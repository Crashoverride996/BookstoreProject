using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetBooksWebCommand : IGetBooksWebCommand
    {
        private readonly BookstoreContext _context;

        public EfGetBooksWebCommand(BookstoreContext context)
        {
            _context = context;
        }

        public IEnumerable<BookDto> Execute(BookSearch request)
        {
            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
            {
                query = query.Where(b => b.Title.ToLower().Contains(request.Title.ToLower()));
            }

            if (request.MinPrice.HasValue)
            {
                query = query.Where(b => b.Price >= request.MinPrice);
            }

            if (request.MaxPrice.HasValue)
            {
                query = query.Where(b => b.Price <= request.MaxPrice);
            }

            query = query.Include(a => a.Author);

            return query.Select(b => new BookDto
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Price = b.Price,
                AuthorName = b.Author.FirstName + " " + b.Author.LastName
            });
        }
    }
}
