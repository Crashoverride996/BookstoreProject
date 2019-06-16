using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Maker : BaseEntity
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public int CountryId { get; set; }

        public Country Country { get; set; }
        public virtual ICollection<Accessory> Accessories { get; set; }
    }
}
