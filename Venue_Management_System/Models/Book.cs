using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Book
    {
        public int Id { get; set; } //ISBN
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public int TotalBooks { get; set; }
        public int TotalBooksAvailable { get; set; }
    }
}