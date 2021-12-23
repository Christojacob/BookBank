using System;
using System.Collections.Generic;

#nullable disable

namespace BookBankLibrary.Models
{
    public partial class Author
    {
        public Author()
        {
            Books = new HashSet<Book>();
        }

        public string AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorCountry { get; set; }
        public string AuthorDescription { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }
}
