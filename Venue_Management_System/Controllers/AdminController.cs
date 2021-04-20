using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Venue_Management_System.Controllers
{
    public class AdminController : Controller
    {
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
    }
}