using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Venue_Management_System.Models;
using Venue_Management_System.ViewModels;
using VenueStatus = Venue_Management_System.Enum.VenueStatus;
using BookStatus = Venue_Management_System.Enum.BookStatus;
using BookVenueCategory = Venue_Management_System.Enum.BookVenueCategory;

namespace Venue_Management_System.Controllers
{
    public class StudentController : Controller
    {
        private ApplicationDbContext _context;

        public StudentController()
        {
            _context = new ApplicationDbContext();
        }
        // GET: Student
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MyGroups()
        {

            var userId = User.Identity.GetUserId();
            var listOfMyGroupsId = _context.GroupMembers.Where(s => s.UserId == userId).ToList();
            var distinctMyGroupId = listOfMyGroupsId.Distinct(new DistinctItemComparer()).ToList();
            var onlyId = distinctMyGroupId.Where(s => s.UserId == userId).Select(g => g.GroupId).ToList();
            var myGroup = _context.Groups.Include(s => s.Student).Include(m => m.GroupMembers).Where(g => onlyId.Contains(g.Id)).ToList();

            for (int i = 0; i < myGroup.Count; i++)
            {

                foreach (var member in myGroup[i].GroupMembers)
                {
                    var student = _context.Students.First(s => s.UserId == member.UserId);

                    member.Student = student;
                }
            }

            return View(myGroup);
        }

        public ActionResult CreateGroup()
        {
            //var student = _context.Students.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateGroup(Group group)
        {
            //validate name no need to create two group with the same
            var userId = User.Identity.GetUserId();

            if (userId == null)
                return HttpNotFound();

            if (!ModelState.IsValid)
            {
                return View(group);
            }
            group.UserId = userId;
            _context.Groups.Add(group);
            _context.SaveChanges();

            var groupId = group.Id;
            var groupOwner = new GroupMember
            {
                GroupId = groupId,
                UserId = userId
            };
            //check if the user is not already added on the groupmember table on the same group
            _context.GroupMembers.Add(groupOwner);
            _context.SaveChanges();
            return RedirectToAction("CreateGroup"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult AddMembers()
        {
            var userId = User.Identity.GetUserId();
            var listOfMyGroups = _context.Groups.Where(s => s.UserId == userId).ToList();

            var groupViewModel = new GroupViewModel
            {
                Groups = listOfMyGroups
            };
            return View(groupViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddMembers(GroupViewModel groupViewModel)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new GroupViewModel
                {
                    Groups = _context.Groups.ToList(),
                    GroupMembers = groupViewModel.GroupMembers,
                    Student = groupViewModel.Student
                };
                return View(viewModel);
            }

            var student = _context.Students.FirstOrDefault(s => s.StudentNumber == groupViewModel.Student.StudentNumber);

            if (student == null)
                return HttpNotFound(); // diplay message student not found

            var groupMember = new GroupMember
            {
                UserId = student.UserId,
                Student = student,
                GroupId = groupViewModel.GroupMembers.Id
            };
            _context.GroupMembers.Add(groupMember);
            _context.SaveChanges();
            return RedirectToAction("MyGroups"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult GetEvent()
        {
            return View("GetEvents");
        }

        public JsonResult GetEvents()
        {
            var events = _context.Events.ToList();
            return new JsonResult { Data = events, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        public ActionResult ViewGroupDetails(int? id)
        {
            var myGroup = _context.Groups.Include(s => s.Student)
                                         .Include(m => m.GroupMembers)
                                         .Where(g => g.Id == id).ToList();

            for(int i = 0; i < myGroup.Count; i++)
            {
                
                foreach(var member in myGroup[i].GroupMembers)
                {
                    var student = _context.Students.First(s => s.UserId == member.UserId);

                    member.Student = student;
                }
            }
            

            return View(myGroup);
        }

        public ActionResult BookVenue()
        {
            //use userId to start displaying lab that are in his or her department
            // check on time if user already left
            var userId = User.Identity.GetUserId();
            var user = _context.Students.First(s => s.UserId == userId);
            var venues = _context.Venues.Include(v => v.VenueType)
                                        .Include(c => c.Campus)
                                        .Where(s => s.venueStatusId == VenueStatus.Active &&
                                               s.NumberOfSitsAvailable > 0 &&
                                               s.departmentId == user.departmentId)
                                        .ToList();
            return View(venues);
        }

        public ActionResult BookSpace(int? id)
        {
            var venueToBook = _context.Venues.FirstOrDefault(v => v.Id == id);
            var userId = User.Identity.GetUserId();
            //check if the lab is active and have available space

            if (venueToBook == null)
                return HttpNotFound();

            //List of group, Venue, User, Date
            var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
            var ownerGroups = _context.Groups.Include(m => m.GroupMembers).Where(s => s.UserId == userId).ToList();
            var categories = _context.BookVenueCategories.ToList();
            var bookVenue = new BookVenue
            {
                UserId = userId,
                VenueId = venueToBook.Id

            };
            var veiwModel = new BookVenueViewModel
            {
                Venue = venueToBook,
                Student = student,
                Groups = ownerGroups,
                BookVenue = bookVenue,
                BookVenueCategories = categories
            };

            
            return View(veiwModel);
        }

        //private List<BookVenueCategory> GetCategoryList()
        //{
        //    //Hard coded a list of items here, You may replace it with 
        //    // Items from your db table
        //    return new List<BookVenueCategory>
        //    {
        //        new BookVenueCategory {Id = 1, BookVenueType = "Individual"},
        //        new BookVenueCategory {Id = 2, BookVenueType = "Group"}
        //    };
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookSpace(BookVenueViewModel bookVenueViewModel)
        {
            var userId = User.Identity.GetUserId();
            
            var selectedCategoty = bookVenueViewModel.SelectedCategory;
            if (!ModelState.IsValid)
            {
                var student = _context.Students.FirstOrDefault(s => s.UserId == userId);
                var ownerGroups = _context.Groups.Include(m => m.GroupMembers).Where(s => s.UserId == userId).ToList();
                var categories = _context.BookVenueCategories.ToList();
                bookVenueViewModel.Groups = ownerGroups;
                bookVenueViewModel.BookVenueCategories = categories;
                return View(bookVenueViewModel);
            }

            var bookVenue = bookVenueViewModel.BookVenue;
            bookVenue.bookVenueCategoryId = (byte)bookVenueViewModel.SelectedCategory;
 
            _context.BookVenues.Add(bookVenue);
            _context.SaveChanges();
            var bookId = bookVenue.Id;

            if (bookVenue.bookVenueCategoryId == BookVenueCategory.Group)
            {
                var groupBooking = new GroupBooking
                {
                    GroupId = bookVenueViewModel.GroupMembers.Id,
                    bookVenueId =bookId
                };
                _context.GroupBookings.Add(groupBooking);
            }
            _context.SaveChanges();

            return RedirectToAction("MyBookings"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult Books()
        {
            var books = _context.Books.Where(s => s.BookStatusId == BookStatus.Active).ToList();

            return View(books);
        }
        //Get venue to book
        public ActionResult BorrowBook(int id)
        {
            var borroweBook = _context.BorrowedBooks.FirstOrDefault(b => b.BookId == id);

            if (borroweBook != null)
                return RedirectToAction("MyBorrowedBooks");

            var userId = User.Identity.GetUserId();
            var book = _context.Books.FirstOrDefault(b => b.Id == id);

            if (book == null)
                return HttpNotFound();

            var borrowBook = new BorrowedBook
            {
                Student = _context.Students.First(u => u.UserId == userId),
                UserId = userId,
                Book = book,
                BookId = book.Id

            };

            return View(borrowBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BorrowBook(BorrowedBook borrowedBook)
        {

            var bookToBorrow = _context.Books.First(b => b.Id == borrowedBook.BookId);

            bookToBorrow.TotalBooksAvailable -= 1;

            _context.SaveChanges();

            if (!ModelState.IsValid)
            {
                return View(borrowedBook);
            }
            _context.BorrowedBooks.Add(borrowedBook);
            _context.SaveChanges();
            return RedirectToAction("MyBorrowedBooks"); // Toast Status succeful changes and redirect to pending applications
        }

        public ActionResult MyBorrowedBooks()
        {
            var userId = User.Identity.GetUserId();
            var bookedBooks = _context.BorrowedBooks.Include(b => b.Book).Where(u => u.UserId == userId).ToList();

            return View(bookedBooks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReturnBook(int id)
        {
            var bookToReturn = _context.BorrowedBooks.FirstOrDefault(b => b.BookId == id);

            if (bookToReturn == null)
                return RedirectToAction("MyBorrowedBooks"); //Toast to say you dont owe this book

            _context.BorrowedBooks.Remove(bookToReturn);

            var book = _context.Books.First(b => b.Id == bookToReturn.BookId);
            book.TotalBooksAvailable += 1;
            _context.SaveChanges();

            return RedirectToAction("MyBorrowedBooks"); // Toast Status succeful returned book
        }
    }
}