using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venue_Management_System.Models;
using Venue_Management_System.Models.Syah_Models;

namespace Venue_Management_System.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext _context;

        public BookController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Books
        public ActionResult Index()
        {
            //var books = _context.Books_Sya.Include(h => h.BorrowHistories)
            //    .Select(b => new Book_Sya
            //    {
            //        Id = b.Id,
            //        Author = b.Author,
            //        Publisher = b.Publisher,

            //        Title = b.Title,
            //        IsAvailable = !b.BorrowHistories.Any(h => h.ReturnDate == null)
            //    }).ToList();

            var allBooks = _context.Books_Sya.Include(h => h.BorrowHistories).ToList();
            return View(allBooks);
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = _context.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookId,Title,Author,Publisher")] Book_Sya book)
        {
            if (ModelState.IsValid)
            {
                _context.Books_Sya.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Sya book = _context.Books_Sya.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookId,Title,Author,Publisher")] Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(book).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book_Sya book = _context.Books_Sya.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book_Sya book = _context.Books_Sya.Find(id);
            _context.Books_Sya.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}