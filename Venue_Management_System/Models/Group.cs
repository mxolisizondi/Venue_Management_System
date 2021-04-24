using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Group
    {
        public byte Id { get; set; }
        public string Name { get; set; }

        public Student Student { get; set; }
        public long StudentNumber { get; set; }
    }
}