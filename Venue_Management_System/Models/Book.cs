﻿using Microsoft.AspNet.Identity;
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
    public class Book
    {
        public int Id { get; set; } //ISBN
        public string Name { get; set; }
        public string Author { get; set; }
        public DateTime DatePublished { get; set; }
        public int TotalBooks { get; set; }
        public int TotalBooksAvailable { get; set; }

        public string Publisher { get; set; }
        public ICollection<BorrowHistory> BorrowHistories { get; set; }
    }
}