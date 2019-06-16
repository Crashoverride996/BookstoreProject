using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreAplication.DTO
{
    public class BookDto : BaseDto
    {
        [Required(ErrorMessage = "You must enter the title")]
        [MinLength(3, ErrorMessage = "Title must have at least 3 characters.")]
        [MaxLength(30, ErrorMessage = "Title shouldn't exceed 30 characters.")]
        public string Title { get; set; }
        [MaxLength(300, ErrorMessage = "Description shouldn't exceed 300 characters.")]
        public string Description { get; set; }
        public double Price { get; set; }
        public string AuthorName { get; set; }
        [Required]
        public string AuthorFirstname { get; set; }
        [Required]
        public string AuthorLastname { get; set; }
    }
}
