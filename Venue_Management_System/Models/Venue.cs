using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Venue
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSits { get; set; }
        [Display(Name = "Sit Allowed")]
        public int NumberOfSitsAllowed { get; set; }
        public int NumberOfSitsAvailable { get; set; }

        public Campus Campus { get; set; }
        public int CampusId { get; set; }

        public VenueType VenueType { get; set; }
        public int VenueTypeId { get; set; }

        public VenueStatus venueStatus { get; set; }
        public byte venueStatusId { get; set; }

        public Department Department { get; set; }
        public int departmentId { get; set; }


    }
}