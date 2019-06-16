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
    public class EfGetAccessoriesWebCommand : IGetAccessoriesWebCommand
    {
        private readonly BookstoreContext _context;

        public EfGetAccessoriesWebCommand(BookstoreContext context)
        {
            _context = context;
        }

        public IEnumerable<AccessoryDto> Execute(AccessorySearch request)
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

            query = query.Include(m => m.Maker);

            return query.Select(a => new AccessoryDto
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                Price = a.Price,
                MakerName = a.Maker.Name,
                CountryName = a.Maker.Country.Name
            });
        }
    }
}
