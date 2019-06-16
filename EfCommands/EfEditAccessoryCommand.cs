using BookstoreAplication.Commands;
using BookstoreAplication.DTO;
using BookstoreAplication.Exceptions;
using EfDataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace EfCommands
{
    public class EfEditAccessoryCommand : IEditAccessoryCommand
    {
        private readonly BookstoreContext _context;

        public EfEditAccessoryCommand(BookstoreContext context)
        {
            _context = context;
        }

        public void Execute(CreateAccessoryDto request)
        {
            var accessory = _context.Accessories.Find(request.Id);

            if (accessory == null)
            {
                throw new EntityNotFoundException("Accessory doesn't exist.");
            }

            accessory.Name = request.Name;
            accessory.Description = request.Description;
            accessory.Price = request.Price;

            _context.SaveChanges();
        }
    }
}
