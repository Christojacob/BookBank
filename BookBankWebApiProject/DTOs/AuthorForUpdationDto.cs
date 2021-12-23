using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.DTOs
{
    public class AuthorForUpdationDto
    {
        [MaxLength(100)]
        public string AuthorName { get; set; }
        [MaxLength(100)]
        public string AuthorCountry { get; set; }
        [MaxLength(500)]
        public string AuthorDescription { get; set; }
    }
}
