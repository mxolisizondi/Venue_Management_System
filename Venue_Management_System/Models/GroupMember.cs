using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class GroupMember
    {
        public byte Id { get; set; }
        public long StudentNumber { get; set; }
        public Group Group { get; set; }
        public byte GroupId { get; set; }
    }
}