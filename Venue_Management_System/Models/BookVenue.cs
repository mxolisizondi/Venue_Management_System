using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class BookVenue
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        public long StudentNumber { get; set; }

        public Venue Venue { get; set; }
        public int VenueId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime LeavingTime { get; set; }

    }
    //book now
}

