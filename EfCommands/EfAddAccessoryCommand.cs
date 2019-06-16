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
    public class EfAddAccessoryCommand : IAddAccessoryCommand
    {
        private readonly BookstoreContext _context;

        public EfAddAccessoryCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(AccessoryDto request)
        {
            if (_context.Accessories.Any(a => a.Name == request.Name))
            {
                throw new EntityAlreadyExistsException();
            }
            if (_context.Makers.Any(m => m.Name == request.MakerName))
            {
                Maker maker = _context.Makers.Where(m => m.Name == request.MakerName).First();
                maker.Country = _context.Country.Where(c => c.Name == request.CountryName).First();
                Accessory accessory = new Accessory
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Maker = maker
                };
                _context.Accessories.Add(accessory);
                _context.SaveChanges();
            }
            else
            {
                Maker maker = new Maker
                {
                    Name = request.MakerName
                };
                maker.Country = _context.Country.Where(c => c.Name == request.CountryName).First();
                Accessory accessory = new Accessory
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    Maker = maker
                };
                _context.Accessories.Add(accessory);
                _context.SaveChanges();
            }
        }
    }
}
