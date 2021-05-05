using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Venue_Management_System.Models;

namespace Venue_Management_System.ViewModels
{
    public class ReportCaseViewModel
    {
        public ReportCase ReportCase { get; set; }
        public IEnumerable<CovidStatus> CovidStatuses { get; set; }
        public IEnumerable<CovidDocument> CovidDocuments { get; set; }

    }
}