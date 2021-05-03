using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Venue_Management_System.Models;

namespace Venue_Management_System.ViewModels
{
    public class VenueViewModel
    {
        public Venue Venue { get; set; }
        public IEnumerable<VenueType> VenueTypes { get; set; }
        public IEnumerable<Campus> Campuses { get; set; }
        public IEnumerable<VenueStatus> VenueStatuses { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}