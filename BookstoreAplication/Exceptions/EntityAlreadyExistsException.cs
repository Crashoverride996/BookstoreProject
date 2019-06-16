using System;
using System.Collections.Generic;
using System.Text;

namespace BookstoreAplication.Exceptions
{
    public class EntityAlreadyExistsException : Exception
    {
        public EntityAlreadyExistsException(string entity) : base($"{entity} already exists.")
        {

        }

        public EntityAlreadyExistsException()
        {

        }
    }
}
