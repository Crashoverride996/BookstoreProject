using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookstoreApi.Queries
{
    public class AuthorSearchQuery
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
