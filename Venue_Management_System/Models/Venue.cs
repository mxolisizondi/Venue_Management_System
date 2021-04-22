using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSits { get; set; }
        public int NumberOfSitsAllowed { get; set; }
        public int NumberOfSitsAvailable { get; set; }

        // Venue on which campus

        public VenueType VenueType { get; set; }
        public int VenueTypeId { get; set; }

    }
}