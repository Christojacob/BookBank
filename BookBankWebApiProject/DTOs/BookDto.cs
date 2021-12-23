using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankWebApiProject.DTOs
{
    public class BookDto
    {
        public string BookIsbn { get; set; }
        public string BookName { get; set; }
        public string BookUrl { get; set; }
        public string BookDescription { get; set; }
        public string BookGenre { get; set; }
    }
}
