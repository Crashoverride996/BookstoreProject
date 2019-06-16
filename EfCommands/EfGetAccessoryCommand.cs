using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfGetAccessoryCommand : IGetAccessoryCommand
    {
        private readonly BookstoreContext _context;

        public EfGetAccessoryCommand(BookstoreContext context)
        {
            _context = context;
        }

        public AccessoryDto Execute(int request)
        {
            var accessory = _context.Accessories.Find(request);

            if (accessory == null)
                throw new EntityNotFoundException("Accessory not found");

            return new AccessoryDto
            {
                Name = accessory.Name,
                Description = accessory.Description,
                Price = accessory.Price,
                MakerName = accessory.Maker.Name,
                CountryName = accessory.Maker.Country.Name
            };
        }
    }
}
