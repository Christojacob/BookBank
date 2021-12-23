using System;
using System.Collections.Generic;

#nullable disable

namespace BookBankLibrary.Models
{
    public partial class Book
    {
        public string BookIsbn { get; set; }
        public string BookName { get; set; }
        public string BookUrl { get; set; }
        public string BookDescription { get; set; }
        public string AuthorId { get; set; }
        public string BookGenre { get; set; }

        public virtual Author Author { get; set; }
    }
}
