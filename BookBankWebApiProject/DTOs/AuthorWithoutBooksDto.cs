using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.DTOs
{
    public class AuthorWithoutBooksDto
    {
        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorCountry { get; set; }
        public string AuthorDescription { get; set; }
    }
}
