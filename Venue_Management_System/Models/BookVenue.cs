﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class BookVenue
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        public string UserId { get; set; }

        public Venue Venue { get; set; }
        public int VenueId { get; set; }

        public DateTime BookingDate { get; set; }

        public DateTime StartingTime { get; set; }

        public DateTime LeavingTime { get; set; }

        public BookVenueCategory BookVenueCategory { get; set; }

        public byte bookVenueCategoryId { get; set; }

    }
    //book now
}

