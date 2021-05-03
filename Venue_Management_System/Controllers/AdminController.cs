using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Venue_Management_System.Models;
using System.Data.Entity;
using Venue_Management_System.ViewModels;
using VenueStatus = Venue_Management_System.Enum.VenueStatus;
using BookStatus = Venue_Management_System.Enum.BookStatus;

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
                                        .Include(s => s.venueStatus)
                                        .ToList();

            return View(venues);
        }

        public ActionResult AddVenue()
        {
            var venueTypes = _context.venueTypes.ToList();
            var campuses = _context.Campuses.ToList();
            var venueStatuses = _context.VenueStatuses.ToList();
            var departments = _context.Departments.ToList();
            var venueViewModel = new VenueViewModel
            {
                VenueTypes = venueTypes,
                Campuses = campuses,
                VenueStatuses = venueStatuses,
                Departments = departments

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
                    Campuses = _context.Campuses.ToList(),
                    VenueStatuses = _context.VenueStatuses.ToList(),
                    Departments = _context.Departments.ToList()
                };
                return View(viewModel);
            }
            venue.NumberOfSitsAvailable = venue.NumberOfSitsAllowed;
            _context.Venues.Add(venue);
            _context.SaveChanges();
            return RedirectToAction("AddVenue"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult AddVenueType()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVenueType(VenueType venueType)
        {
            if (!ModelState.IsValid)
            {
                return View(venueType);
            }
            _context.venueTypes.Add(venueType);
            _context.SaveChanges();
            return RedirectToAction("Venues"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult AddCampus()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCampus(Campus campus)
        {
            if (!ModelState.IsValid)
            {
                return View(campus);
            }
            _context.Campuses.Add(campus);
            _context.SaveChanges();
            return RedirectToAction("Venues"); // Toast Status succeful 
        }

        //public ActionResult UpdateVenue()
        //{
        //    var venues = _context.Venues.Include(v => v.VenueType)
        //                                .Include(c => c.Campus)
        //                                .Include(s => s.venueStatus)
        //                                .Where(s => s.venueStatusId == VenueStatus.InActive)
        //                                .ToList();
        //    return View("Venues",venues);
        //}

        //GET: View Venue


        public ActionResult ViewVenue(int id)
        {

            var venue = _context.Venues.Include(v => v.VenueType)
                                             .Include(c => c.Campus)
                                             .Include(s => s.venueStatus)
                                             .SingleOrDefault(v => v.Id == id);

            if (venue == null) 
                return HttpNotFound();


            return View(venue);
        }

        public ActionResult UpdateVenue(int? id)
        {
            var venue = _context.Venues.Include(v => v.VenueType)
                                             .Include(c => c.Campus)
                                             .Include(s => s.venueStatus)
                                             .SingleOrDefault(v => v.Id == id);

            if (venue == null)
                return HttpNotFound();

            var venueViewModel = new VenueViewModel
            {
                Venue = venue,
                Campuses = _context.Campuses.ToList(),
                VenueTypes = _context.venueTypes.ToList(),
                VenueStatuses = _context.VenueStatuses.ToList()
            };

            return View(venueViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVenue(Venue venue)
        {
            var venueInDB = _context.Venues.SingleOrDefault(v => v.Id == venue.Id);

            if (venueInDB == null)
                return HttpNotFound(); // Toast that the school was not deleted because was found

            venueInDB.Name = venue.Name;
            venueInDB.NumberOfSits = venue.NumberOfSits;
            venueInDB.NumberOfSitsAllowed = venue.NumberOfSitsAllowed;
            venueInDB.NumberOfSitsAvailable = venue.NumberOfSitsAvailable;
            venueInDB.VenueTypeId = venue.VenueTypeId;
            venueInDB.CampusId = venue.CampusId;
            venueInDB.venueStatusId = venue.venueStatusId;
            _context.SaveChanges();
            return RedirectToAction("Venues"); // Toast Status succeful
        }


        public ActionResult RemoveVenue()
        {
            return RedirectToAction("Venues");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RemoveVenue(int? id)
        {
            var venue = _context.Venues.SingleOrDefault(v => v.Id == id);

            if (venue == null)
                return HttpNotFound(); // Toast that the school was not deleted because was found
            venue.venueStatusId = VenueStatus.InActive;
            _context.SaveChanges();
            return RedirectToAction("Venues"); // Toast Status succeful
        }

        // GET: Books/Get Active Books
        public ActionResult GetAllBooks()
        {
            var books = _context.Books.Where(b => b.BookStatusId == BookStatus.Active).ToList();
            return View(books);
        }

        // GET: Books/GetAllBooks
        public ActionResult GetInActiveBooks()
        {
            var inActiveBooks = _context.Books.Where(b => b.BookStatusId == BookStatus.InActive).ToList();
            return View(inActiveBooks);
        }

        // GET: Books/Create
        public ActionResult CreateBook()
        {
            var veiwModel = new BookViewModel
            {
                BookStatuses = _context.BookStatuses.ToList()
            };
            return View(veiwModel);
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            book.TotalBooksAvailable = book.TotalBooks;
            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("GetAllBooks");
        }

        // GET: Books/Create
        public ActionResult UpdateBook(int? id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return HttpNotFound();

            var viewModel = new BookViewModel
            {
                Book = book,
                BookStatuses = _context.BookStatuses.ToList()
            };

            return View(viewModel);
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateBook(Book book)
        {
            var bookInDb = _context.Books.First(b => b.Id == book.Id);

            bookInDb.Name = book.Name;
            bookInDb.Author = book.Author;
            bookInDb.DatePublished = book.DatePublished;
            bookInDb.TotalBooks = book.TotalBooks;
            bookInDb.BookStatusId = book.BookStatusId;
            if (!ModelState.IsValid)
            {
                return View(book);
            }
            _context.SaveChanges(); // update succesful

            return RedirectToAction("GetAllBooks");
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteBook(int? id)
        {
            var bookInDb = _context.Books.First(b => b.Id == id);

            bookInDb.BookStatusId = BookStatus.InActive;

            _context.SaveChanges(); // update succesful

            return RedirectToAction("GetAllBooks");
        }

        // GET: Books/Create
        public ActionResult ViewBook(int? id)
        {
            var book = _context.Books.Include(s => s.BookStatus).FirstOrDefault(b => b.Id == id);

            if (book == null)
                return HttpNotFound();

            return View(book);
        }

    }
}