using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Responses;
using BookstoreAplication.Searches;
using EfDataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfCommands
{
    public class EfGetBooksCommand : IGetBooksCommand
    {
        private readonly BookstoreContext _context;

        public EfGetBooksCommand(BookstoreContext context)
        {
            _context = context;
        }

        public PagedResponse<BookDto> Execute(BookSearch request)
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

            var totalCount = query.Count();

            query = query.Include(a => a.Author).Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PagedResponse<BookDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(b => new BookDto
                {
                    Id = b.Id,
                    Title = b.Title,
                    Description = b.Description,
                    Price = b.Price,
                    AuthorName = b.Author.FirstName + " " + b.Author.LastName
                })
            };

            return response;
        }
    }
}
