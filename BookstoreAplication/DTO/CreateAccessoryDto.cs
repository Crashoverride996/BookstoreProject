using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreAplication.DTO
{
    public class CreateAccessoryDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        [Required]
        [Range(0, 1000)]
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string MakerName { get; set; }
        public string CountryName { get; set; }
    }
}
