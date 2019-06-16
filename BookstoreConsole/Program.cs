using Microsoft.EntityFrameworkCore;
using System;
using Domain;
using EfDataAccess;
using BookstoreAplication.Commands;
using EfCommands;
using BookstoreAplication.Exceptions;

namespace BookstoreConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BookstoreContext();
            var country1 = new Country { Name = "Serbia" };
            var country2 = new Country { Name = "Italy" };
            var maker1 = new Maker { Name = "Memento Mori ", Address = "Cara Dusana bb", Country = country1 };
            var maker2 = new Maker { Name = "Betelgeuse", Address = "Betelgeuse 42", Country = country2 };
            var accessory1 = new Accessory { Name = "Superman Mug", Description = "Ceramic Mug", Price = 15, Maker = maker1 };
            var accessory2 = new Accessory { Name = "Batman Mug", Description = "Ceramic Mug", Price = 12, Maker = maker2 };
            context.Country.Add(country1);
            context.Country.Add(country2);
            context.Makers.Add(maker1);
            context.Makers.Add(maker2);
            context.Accessories.Add(accessory1);
            context.Accessories.Add(accessory2);

            context.SaveChanges();
        }
    }
}
