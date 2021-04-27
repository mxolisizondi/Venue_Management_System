using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Venue_Management_System.Models;

namespace Venue_Management_System.ViewModels
{
    public class GroupMemberViewModel
    { 
        public ICollection<Group> Groups { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
    }
}