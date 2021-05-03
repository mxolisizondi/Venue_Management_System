using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class GroupBooking
    {
        public int Id { get; set; }

        public Group Group { get; set; }
        public int GroupId { get; set; }

        public BookVenue BookVenue { get; set; }
        public int bookVenueId { get; set; }
    }
}