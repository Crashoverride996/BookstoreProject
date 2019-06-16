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
    public class EfGetAuthorsCommand : IGetAuthorsCommand
    {
        private readonly BookstoreContext _context;

        public EfGetAuthorsCommand(BookstoreContext context)
        {
            _context = context;
        }

        public PagedResponse<AuthorDto> Execute(AuthorSearch request)
        {
            var query = _context.Authors.AsQueryable();
            
            if (!string.IsNullOrEmpty(request.FirstName))
            {
                query = query.Where(a => a.FirstName == request.FirstName);
            }

            if (!string.IsNullOrEmpty(request.LastName))
            {
                query = query.Where(a => a.LastName == request.LastName);
            }

            var totalCount = query.Count();

            query = query.Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PagedResponse<AuthorDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(a => new AuthorDto
                {
                    Id = a.Id,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                })
            };

            return response;
        }
    }
}
