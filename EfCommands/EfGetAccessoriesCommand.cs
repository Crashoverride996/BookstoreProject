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
    public class EfGetAccessoriesCommand : IGetAccessoriesCommand
    {
        private readonly BookstoreContext _context;

        public EfGetAccessoriesCommand(BookstoreContext context)
        {
            _context = context;
        }

        public PagedResponse<AccessoryDto> Execute(AccessorySearch request)
        {
            var query = _context.Accessories.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(a => a.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (request.Price > 0)
            {
                query = query.Where(b => b.Price == request.Price);
            }

            var totalCount = query.Count();

            query = query.Include(m => m.Maker).Skip((request.PageNumber - 1) * request.PerPage).Take(request.PerPage);

            var pagesCount = (int)Math.Ceiling((double)totalCount / request.PerPage);

            var response = new PagedResponse<AccessoryDto>
            {
                CurrentPage = request.PageNumber,
                TotalCount = totalCount,
                PagesCount = pagesCount,
                Data = query.Select(a => new AccessoryDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Price = a.Price,
                    MakerName = a.Maker.Name,
                    CountryName = a.Maker.Country.Name
                })
            };

            return response;
        }
    }
}
