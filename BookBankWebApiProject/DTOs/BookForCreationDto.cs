using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.DTOs
{
    public class BookForCreationDto
    {
        [Required(ErrorMessage = "You should provide BookIsbn.")]
        [MaxLength(50)]
        public string BookIsbn { get; set; }
        [MaxLength(200)]
        public string BookName { get; set; }
        [MaxLength(200)]
        public string BookUrl { get; set; }
        [MaxLength(500)]
        public string BookDescription { get; set; }
        [MaxLength(200)]
        public string BookGenre { get; set; }
    }
}
