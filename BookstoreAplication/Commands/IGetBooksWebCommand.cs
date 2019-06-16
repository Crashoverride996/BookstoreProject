using BookstoreAplication.DTO;
using BookstoreAplication.Interfaces;
using BookstoreAplication.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Commands
{
    public interface IGetBooksWebCommand : ICommand<BookSearch, IEnumerable<BookDto>>
    {
    }
}
