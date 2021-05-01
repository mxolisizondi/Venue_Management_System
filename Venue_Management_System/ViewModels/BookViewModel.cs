using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Venue_Management_System.Models;

namespace Venue_Management_System.ViewModels
{
    public class BookViewModel
    {
        public Book Book { get; set; }
        public IEnumerable<BookStatus> BookStatuses { get; set; }
    }
}