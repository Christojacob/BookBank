using BookBankLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBankClient.Models
{
    public class AuthorWithBooks
    {

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorCountry { get; set; }
        public string AuthorDescription { get; set; }

        public int NumberOfBooks { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
