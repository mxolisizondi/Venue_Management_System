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
            var venues = _context.Venues.Include(v => v.VenueType)
                                        .Include(c => c.Campus)
                                        .ToList();

            return View(venues);
        }

        public ActionResult AddVenue()
        {
            var venueTypes = _context.venueTypes.ToList();
            var campuses = _context.Campuses.ToList();
            var venueViewModel = new VenueViewModel
            {
                VenueTypes = venueTypes,
                Campuses = campuses
            };
            return View(venueViewModel);
        }

        //Venue on which campus

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVenue(Venue venue)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new VenueViewModel
                {
                    Venue = venue,
                    VenueTypes = _context.venueTypes.ToList(),
                    Campuses = _context.Campuses.ToList()
                };
                return View(viewModel);
            }
            venue.NumberOfSitsAvailable = venue.NumberOfSitsAllowed;
            _context.Venues.Add(venue);
            _context.SaveChanges();
            return RedirectToAction("AddVenue"); // Toast Status succeful changes and redirect to pending applications
        }
    }
}