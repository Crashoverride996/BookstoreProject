using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Accessory : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int MakerId { get; set; }

        public Maker Maker { get; set; }
    }
}
