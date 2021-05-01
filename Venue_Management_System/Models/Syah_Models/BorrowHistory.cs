using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models.Syah_Models
{
    public class BorrowHistory
    {
        public int BorrowHistoryId { get; set; }

        [Required]
        [Display(Name = "Book")]
        public int BookId { get; set; }

        public Book Book { get; set; }

        [Required]
        [Display(Name = "Firstname")]
        public int UserId { get; set; }

        public Student Student{ get; set; }

        [Display(Name = "Borrow Date")]
        public DateTime BorrowDate { get; set; }

        [Display(Name = "Return Date")]
        public DateTime? ReturnDate { get; set; }
    }
}