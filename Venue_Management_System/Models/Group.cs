using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class Group
    {
        [Display(Name ="Choose groups")]
        public int Id { get; set; }
        public string Name { get; set; }

        public Student Student { get; set; }
        public string UserId { get; set; }

        public ICollection<GroupMember> GroupMembers { get; set; }
        //public ICollection<Student> Students { get; set; }
    }
}