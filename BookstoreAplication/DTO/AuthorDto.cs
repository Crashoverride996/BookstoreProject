using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreAplication.DTO
{
    public class AuthorDto : BaseDto
    {
        [Required]
        [MaxLength(15)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(25)]
        public string LastName { get; set; }

        public IEnumerable<string> BookTitles { get; set; }
    }
}
