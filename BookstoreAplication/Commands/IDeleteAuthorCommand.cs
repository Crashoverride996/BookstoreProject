﻿using BookstoreAplication.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Commands
{
    public interface IDeleteAuthorCommand : ICommand<int>
    {
    }
}
