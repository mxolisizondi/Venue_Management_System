using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Venue_Management_System.Models
{
    public class ReportCase
    {
        public int Id { get; set; }

        public Student Student { get; set; }
        public string UserId { get; set; }

        public CovidStatus CovidStatus { get; set; }
        public byte covidStatusId { get; set; }

        [Column(TypeName = "date")]
        public DateTime TestDate { get; set; }

        public ICollection<CovidDocument> CovidDocuments { get; set; }

    }
}