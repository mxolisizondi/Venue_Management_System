using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Venue_Management_System.Models
{
    public class CovidDocument
    {
        public int Id { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }

        public Student Student { get; set; }

        public string UserId { get; set; }

        public ReportCase ReportCase { get; set; }

        public int reportCaseId { get; set; }
    }
}