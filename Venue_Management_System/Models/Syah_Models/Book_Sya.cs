using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models.Syah_Models
{
    public class Book_Sya
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public bool IsAvailable { get; set; }

        public string Publisher { get; set; }
        public ICollection<BorrowHistory> BorrowHistories { get; set; }
    }
}