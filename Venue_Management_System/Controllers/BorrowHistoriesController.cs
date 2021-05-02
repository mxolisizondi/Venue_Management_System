using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Venue_Management_System.Models;
using Venue_Management_System.ViewModels;
using System.Web.UI.WebControls;

namespace Venue_Management_System.Controllers
{
    public class BorrowHistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BorrowHistories/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var borrowHistory = new BorrowHistory { BookId = book.Id, BorrowDate = DateTime.Now, Book = book };
            ViewBag.UserId = new SelectList(db.Students, "UserId", "Firstname");
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.BorrowHistories.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }

            ViewBag.CustomerId = new SelectList(db.Students, "UserId", "FirstName", borrowHistory.UserId);
            borrowHistory.Book = db.Books.Find(borrowHistory.BookId);
            return View(borrowHistory);
        }

        // GET: BorrowHistories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistories
                .Include(b => b.Book)
                .Include(c => c.Customer)
                .Where(b => b.BookId == id && b.ReturnDate == null)
                .FirstOrDefault();
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistories/Edit/5
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,BookId,UserId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = db.BorrowHistories.Include(i => i.Book)
                    .FirstOrDefault(i => i.BorrowHistoryId == borrowHistory.BorrowHistoryId);
                if (borrowHistoryItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                borrowHistoryItem.ReturnDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Books");
            }
            return View(borrowHistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

