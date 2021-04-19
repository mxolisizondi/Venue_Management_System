using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Staff
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UseId { get; set; }

        public long StaffNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}