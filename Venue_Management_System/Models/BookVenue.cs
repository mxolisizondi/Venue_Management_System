using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venue_Management_System.Models
{
    public class BookVenue
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        public string UserId { get; set; }

        public Venue Venue { get; set; }
        public int VenueId { get; set; }

        [Column(TypeName = "date")]
        public DateTime BookingDate { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime StartingTime { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
        public DateTime LeavingTime { get; set; }

        public BookVenueCategory BookVenueCategory { get; set; }

        public byte bookVenueCategoryId { get; set; }

    }
    //book now
}

