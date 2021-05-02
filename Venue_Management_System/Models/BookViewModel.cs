using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Venue_Management_System.Models;
using Venue_Management_System.ViewModels;
using System.Web.UI.WebControls;

namespace Venue_Management_System.Models
{
    public class BookViewModel
    {
        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Publisher { get; set; }

        public bool IsAvailable { get; set; }
    }
}