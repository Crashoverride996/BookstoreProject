using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Searches
{
    public class BaseSearch
    {
        public int PerPage { get; set; } = 5;
        public int PageNumber { get; set; } = 1;
    }
}
