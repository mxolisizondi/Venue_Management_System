using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Student
    {
        [Key, ForeignKey("ApplicationUser")]
        public string UserId { get; set; }

        public long StudentNumber { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string CellNumber { get; set; }
        public string Email { get; set; }
        public Department Department { get; set; }
        public int departmentId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
    }
}