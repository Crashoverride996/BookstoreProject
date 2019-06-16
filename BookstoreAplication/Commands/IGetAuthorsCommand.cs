﻿using BookstoreAplication.DTO;
using BookstoreAplication.Interfaces;
using BookstoreAplication.Responses;
using BookstoreAplication.Searches;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Commands
{
    public interface IGetAuthorsCommand : ICommand<AuthorSearch, PagedResponse<AuthorDto>>
    {
    }
}
