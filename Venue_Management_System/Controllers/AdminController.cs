using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Venue_Management_System.Models;
using System.Data.Entity;
using Venue_Management_System.ViewModels;

namespace Venue_Management_System.Controllers
{
    public class AdminController : Controller
    {
        private ApplicationDbContext _context;

        public AdminController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        // GET: Admin
        public ActionResult Bookings()
        {
            return View();
        }

        //wwww.venuemagementsystem.co.za/Admin/VeiwStudentDetails
        public ActionResult ViewStudentDetails()
        {
            //code to get all students from Database
            return View();
        }

        // GET: Admin
        public ActionResult Venues()
        {
            var venues = _context.Venues.Include(v => v.VenueType).ToList();

            return View(venues);
        }

        public ActionResult AddVenue()
        {
            var venueTypes = _context.venueTypes.ToList();
            var venueViewModel = new VenueViewModel
            {
                VenueTypes = venueTypes
            };
            return View(venueViewModel);
        }

        //Venue on which campus
    }
}