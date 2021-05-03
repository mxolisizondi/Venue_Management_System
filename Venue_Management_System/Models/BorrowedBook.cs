using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class BorrowedBook
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public int BookId { get; set; }

        public DateTime DateBorrowed { get; set; }

        public DateTime? DateReturned { get; set; }

        public DateTime DueDate { get; set; }

        //public BorrowedStatus borrowedStatus { get; set; }
        //public int BorrowedStatusId { get; set; }
    }
}