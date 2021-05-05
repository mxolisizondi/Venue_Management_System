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
using CovidStatus = Venue_Management_System.Enum.CovidStatus;
using System.IO;
using System.Net.Mail;

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
            var myBookings = _context.BookVenues.Include(v => v.Venue).Include(c => c.BookVenueCategory).ToList();
            return new JsonResult { Data = myBookings, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
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

        public ActionResult MyBookings()
        {
            var userId = User.Identity.GetUserId();
            var todayDate = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;

            

            var myBookings = _context.BookVenues.Include(v => v.Venue)
                                                .Include(c => c.BookVenueCategory)
                                                .Where(u => u.UserId == userId &&
                                                       u.BookingDate >= todayDate)
                                                .ToList();

            List<BookVenue> activeBookings = new List<BookVenue>();

            foreach (var bookvenue in myBookings)
            {
                if(bookvenue.LeavingTime.TimeOfDay > time || bookvenue.BookingDate > todayDate)
                {
                    activeBookings.Add(bookvenue);
                }
            }
            return View(activeBookings);
        }

        public ActionResult Reschedule(int? id)
        {
            var venueToReschedule = _context.BookVenues.FirstOrDefault(v => v.Id == id);
            //var userId = User.Identity.GetUserId();
            //check if the lab is active and have available space

            if (venueToReschedule == null)
                return HttpNotFound();


            return View(venueToReschedule);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reschedule(BookVenue bookVenue)
        {
            var bookVenueInDB = _context.BookVenues.FirstOrDefault(b => b.Id == bookVenue.Id);

            if (bookVenueInDB == null)
                return HttpNotFound();

            bookVenueInDB.BookingDate = bookVenue.BookingDate;
            bookVenueInDB.LeavingTime = bookVenue.LeavingTime;
            bookVenueInDB.StartingTime = bookVenue.StartingTime;

            _context.SaveChanges();

            return RedirectToAction("MyBookings"); // Toast Status succeful changes and redirect to pending applications
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CancelBooking(int id)
        {
            var userId = User.Identity.GetUserId();
            var todayDate = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;

            var myBooking = _context.BookVenues.FirstOrDefault(u => u.UserId == userId && u.BookingDate >= todayDate && u.Id == id);

            if (myBooking == null)
                return HttpNotFound();

            if (myBooking.LeavingTime.TimeOfDay > time)
            {
                _context.BookVenues.Remove(myBooking);
                _context.SaveChanges();
                //Toast booking canceled successful
                return RedirectToAction("MyBookings");
            }
            //Toast Not able to Remove
            return RedirectToAction("MyBookings");
        }

        public ActionResult MyHistoryBookings()
        {
            var userId = User.Identity.GetUserId();
            var todayDate = DateTime.Today;
            var time = DateTime.Now.TimeOfDay;



            var myHistoryBookings = _context.BookVenues.Include(v => v.Venue)
                                                .Include(c => c.BookVenueCategory)
                                                .Where(u => u.UserId == userId &&
                                                       u.BookingDate < todayDate)
                                                .ToList();

            //List<BookVenue> activeBookings = new List<BookVenue>();

            //foreach (var bookvenue in myBookings)
            //{
            //    if (bookvenue.LeavingTime.TimeOfDay > time)
            //    {
            //        activeBookings.Add(bookvenue);
            //    }
            //}
            return View(myHistoryBookings);
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

        public ActionResult ReportCovidCase()
        {
            var userId = User.Identity.GetUserId();
            var covidStatuses = _context.CovidStatuses.ToList();
            var student = _context.Students.First(u => u.UserId == userId);

            var reportCase = new ReportCase
            {
                Student = student,
                UserId = userId
            };

            var reportCaseViewModel = new ReportCaseViewModel
            {
                ReportCase = reportCase,
                CovidStatuses = covidStatuses
            };

            return View(reportCaseViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReportCovidCase(ReportCase reportCase)
        {
            List<CovidDocument> covidDocuments = new List<CovidDocument>();

            _context.ReportCases.Add(reportCase);
            _context.SaveChanges();


            var reportCaseId = reportCase.Id;
            //upload files
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                byte[] bytes;
                BinaryReader br = new BinaryReader(file.InputStream);

                bytes = br.ReadBytes(file.ContentLength);

                if (file != null && file.ContentLength > 0)
                {
                    var fileData = new CovidDocument
                    {
                        FileName = Path.GetFileName(file.FileName),
                        ContentType = file.ContentType,
                        Data = bytes,
                        UserId = reportCase.UserId,
                        reportCaseId = reportCaseId

                    };
                    covidDocuments.Add(fileData);
                }

            }
            foreach (var file in covidDocuments)
            {
                file.reportCaseId = reportCaseId;
                _context.CovidDocuments.Add(file);
                _context.SaveChanges();
            }
            DeActivateVenue(reportCaseId, covidDocuments);
            return RedirectToAction("MyReportedCases"); // Toast Status succeful returned book
        }

        private void DeActivateVenue(int reportCaseId, List<CovidDocument> covidDocuments)
        {
            //SendEmailToAdmin About this case
            NotifyAdminAboutPositiveCase(reportCaseId, covidDocuments);

            //DeActivate Labs
            //var studentTestedPosite = _context.ReportCases.Include(s => s.Student).Include(c => c.CovidStatus).First(u => u.Id == reportCaseId);
            //if(studentTestedPosite.covidStatusId == CovidStatus.Positive)
            //{
            //    var bookingIds = _context.BookVenues.Where(u => u.UserId == studentTestedPosite.UserId).ToList();
            //    DateTime foteenDaysBack = studentTestedPosite.TestDate.AddDays(-14);
            //    foreach (var bookingId in bookingIds)
            //    {
            //        if(bookingId.BookingDate <= studentTestedPosite.TestDate && bookingId.BookingDate >= foteenDaysBack)
            //        {
            //            var venueToDeActivate = _context.Venues.First(v => v.Id == bookingId.VenueId);
            //            venueToDeActivate.venueStatusId = VenueStatus.InActive;
            //        }
            //        _context.SaveChanges();

            //    }

            //}
            





            //CancelAllPendingBookings(reportCaseId);

            //Notify all prevous contacts
           NotifyAllPrevousContact(reportCaseId);
        }

        private void NotifyAdminAboutPositiveCase(int reportCaseId, List<CovidDocument> covidDocuments)
        {
            var userId = User.Identity.GetUserId();
            var studentTestedPosite = _context.ReportCases.Include(s => s.Student).Include(c => c.CovidStatus).First(u => u.Id == reportCaseId);
            if (studentTestedPosite.covidStatusId == CovidStatus.Positive)
            {
                DateTime foteenDaysBack = studentTestedPosite.TestDate.AddDays(-14);
                var listOfVenueId = _context.BookVenues.Where(u => u.UserId == userId && u.BookingDate > foteenDaysBack &&
                                                                                 u.BookingDate <= studentTestedPosite.TestDate)
                                                                                  .ToList();
                var distinctVenueId = listOfVenueId.Distinct(new DistinctIVenueComparer()).ToList();
                var onlyVenueId = distinctVenueId.Where(u => u.UserId == userId).Select(v => v.VenueId).ToList();

                var venuesUsed = _context.Venues.Where(v => onlyVenueId.Contains(v.Id)).ToList();

                var listOfVenues = "";
                foreach (var venue in venuesUsed)
                {
                    listOfVenues += venue.Name + ", ";
                }
                MemoryStream ms = new MemoryStream(covidDocuments[0].Data);
                var adminEmails = "siyamkhize19@gmail.com,mxolisizondi20@outlook.com";
                MailMessage mail = new MailMessage("mxolisizondi20@outlook.com", adminEmails);
                mail.Subject = "NEW COVID 19 CASE AT DUT";
                mail.Body = "Dear Admin\nWe would like to notify you that they is a student that might have tested positive for covid 19\n" +
                    "Please check close contact on the system also notify cleaners to fumigate the venue\n. These are the venues we believe might be contaminated " + listOfVenues +
                    "Thank You\n" +
                    "Regards Venue Management System.";
                //mail.Attachments.Add(covidDocuments.);
                mail.Attachments.Add(new Attachment(ms, covidDocuments[0].FileName, covidDocuments[0].ContentType));
                SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);

                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "mxolisizondi20@outlook.com",
                    Password = "Mxolisi@1998"
                };
                client.EnableSsl = true;
                {
                    //client.Send(mail);
                }

                CancelAllPendingBookings(reportCaseId);
            }

            return;
        }

        private void CancelAllPendingBookings(int reportCaseId)
        {
            //SendEmailToAdmin About this case
            var userId = User.Identity.GetUserId();
            var studentTestedPosite = _context.ReportCases.Include(s => s.Student).Include(c => c.CovidStatus).First(u => u.Id == reportCaseId);
            DateTime foteenDaysBack = studentTestedPosite.TestDate.AddDays(-14);
           


            var activeBookings = _context.BookVenues.Include(v => v.Venue)
                                                .Include(c => c.BookVenueCategory)
                                                .Where(u => u.BookingDate >=studentTestedPosite.TestDate)
                                                .ToList();


            _context.BookVenues.RemoveRange(activeBookings);
            _context.SaveChanges();

        }

        private void NotifyAllPrevousContact(int reportCaseId)
        {
            //SendEmailToAdmin About this case
            var userId = User.Identity.GetUserId();
            var studentTestedPosite = _context.ReportCases.Include(s => s.Student).Include(c => c.CovidStatus).First(u => u.Id == reportCaseId);
            DateTime foteenDaysBack = studentTestedPosite.TestDate.AddDays(-14);



            var myHistoryBookings = _context.BookVenues.Include(v => v.Venue)
                                    .Include(c => c.BookVenueCategory)
                                    .Include(u => u.Student)
                                    .Where(u => u.BookingDate < studentTestedPosite.TestDate && u.BookingDate > foteenDaysBack)
                                    .ToList();

            var listOfVenueId = _context.BookVenues.Where(u => u.UserId == userId && u.BookingDate > foteenDaysBack &&
                                                                                 u.BookingDate <= studentTestedPosite.TestDate)
                                                                                  .ToList();
            var distinctStudentId = myHistoryBookings.Distinct(new DistinctIVenueComparer()).ToList();
            var onlyStudentId = distinctStudentId.Where(u => u.BookingDate < studentTestedPosite.TestDate && u.BookingDate > foteenDaysBack)
                                                 .Select(v => v.Student.Email).ToList();
      

            var studentIds = _context.Students.Where(u => onlyStudentId.Contains(u.Email)).ToList();

            var ListsOfStudentsToNotify = "";
            foreach(var email in studentIds)
            {
                ListsOfStudentsToNotify += email.Email + ",";
            }
            ListsOfStudentsToNotify = ListsOfStudentsToNotify.Substring(0, ListsOfStudentsToNotify.Length - 1);
            //SendEmail To All Usersmax(Status)
            MailMessage mail = new MailMessage("mxolisizondi20@outlook.com", ListsOfStudentsToNotify);
            mail.Subject = "NEW COVID 19 CASE AT DUT";
            mail.Body = "Dear Student,\n\nWe would like to notify you that they is a student that might have tested positive for covid 19\n\n" +
                "Please check close contact on the system also notify cleaners to fumigate the venue\n. These are the venues we believe might be contaminated \n\n"+""+ 
                "Thank You\n" +
                "Regards Venue Management System.";
            //mail.Attachments.Add(covidDocuments.);
            SmtpClient client = new SmtpClient("smtp-mail.outlook.com", 587);

            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential()
            {
                UserName = "mxolisizondi20@outlook.com",
                Password = "Mxolisi@1998"
            };
            client.EnableSsl = true;
            {
                client.Send(mail);
            }
            return;
        }
    }
}