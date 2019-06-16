using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Searches
{
    public class BookSearch : BaseSearch
    {
        public string Title { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }
    }
}
