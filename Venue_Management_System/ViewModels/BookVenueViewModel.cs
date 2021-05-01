using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Venue_Management_System.Models;

namespace Venue_Management_System.ViewModels
{
    public class BookVenueViewModel
    {
        public Venue Venue { get; set; }
        public Student Student { get; set; }
        public BookVenue BookVenue { get; set; }
        public GroupMember GroupMembers { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}